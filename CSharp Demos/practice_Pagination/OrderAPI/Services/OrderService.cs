using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.DTOs;
using OrderAPI.Models;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto)
        {
            // Get customer and product
            var customer = await _db.Customers.FindAsync(dto.CustomerId);
            var product = await _db.Products.FindAsync(dto.ProductId);

            if (customer == null)
                throw new Exception("Customer not found");
            
            if (product == null)
                throw new Exception("Product not found");
            
            if (product.StockQuantity < dto.Quantity)
                throw new Exception("Insufficient stock");

            // BUSINESS LOGIC STARTS HERE
            
            // 1. Calculate SubTotal
            decimal subTotal = product.UnitPrice * dto.Quantity;
            
            // 2. Calculate Discount based on customer type
            decimal discount = 0;
            if (customer.CustomerType == "Regular")
                discount = subTotal * 0.05m;   // 5% discount
            else if (customer.CustomerType == "Premium")
                discount = subTotal * 0.10m;   // 10% discount
            else if (customer.CustomerType == "VIP")
                discount = subTotal * 0.15m;   // 15% discount
            
            // 3. Calculate Tax (10% on amount after discount)
            decimal afterDiscount = subTotal - discount;
            decimal tax = afterDiscount * 0.10m;
            
            // 4. Calculate Final Amount
            decimal totalAmount = afterDiscount + tax;

            // Create order
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = product.UnitPrice,
                SubTotal = subTotal,
                Discount = discount,
                Tax = tax,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now
            };

            // Update stock
            product.StockQuantity -= dto.Quantity;
            
            // Save to database
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // Return response
            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                CustomerName = customer.CustomerName,
                ProductName = product.ProductName,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice,
                SubTotal = order.SubTotal,
                Discount = order.Discount,
                Tax = order.Tax,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate
            };
        }

        public async Task<PagedResponseDto<OrderResponseDto>> GetOrdersAsync(OrderFilterDto filter)
        {
            // Start with query
            var query = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .AsQueryable();

            // Apply filters
            if (filter.CustomerId.HasValue)
                query = query.Where(o => o.CustomerId == filter.CustomerId.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(o => o.OrderDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.OrderDate <= filter.ToDate.Value);

            // Apply sorting
            if (filter.SortBy?.ToLower() == "totalamount")
            {
                if (filter.SortDirection?.ToLower() == "desc")
                    query = query.OrderByDescending(o => o.TotalAmount);
                else
                    query = query.OrderBy(o => o.TotalAmount);
            }
            else // Default sort by OrderDate
            {
                if (filter.SortDirection?.ToLower() == "asc")
                    query = query.OrderBy(o => o.OrderDate);
                else
                    query = query.OrderByDescending(o => o.OrderDate);
            }

            // Get total count
            int totalRecords = await query.CountAsync();

            // Apply pagination
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            var orders = await query
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

            // Convert to DTO
            var data = orders.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                CustomerName = o.Customer?.CustomerName ?? "",
                ProductName = o.Product?.ProductName ?? "",
                Quantity = o.Quantity,
                UnitPrice = o.UnitPrice,
                SubTotal = o.SubTotal,
                Discount = o.Discount,
                Tax = o.Tax,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate
            }).ToList();

            // Build response
            return new PagedResponseDto<OrderResponseDto>
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                HasPreviousPage = filter.PageNumber > 1,
                HasNextPage = filter.PageNumber < (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                Data = data
            };
        }
    }
}