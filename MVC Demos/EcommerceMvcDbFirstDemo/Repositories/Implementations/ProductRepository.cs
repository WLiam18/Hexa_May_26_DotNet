using EcommerceMvcDbFirstDemo.Data;
using EcommerceMvcDbFirstDemo.Models;
using EcommerceMvcDbFirstDemo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcDbFirstDemo.Repositories.Implementations
{
    public class ProductRepository:IProductRepository
    {
      
            private readonly EcommerceMvcDbContext _context;

            public ProductRepository(EcommerceMvcDbContext context)
            {
                _context = context;
            }

            public async Task<List<Product>> GetAllProductsAsync()
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .ToListAsync();
            }

            public async Task<Product?> GetProductByIdAsync(int productId)
            {
                return await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductId == productId);
            }

            public async Task<Product?> GetProductByIdWithCategoryAsync(int productId)
            {
                return await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);
            }

            public async Task AddProductAsync(Product product)
            {
                await _context.Products.AddAsync(product);
            }

            public void UpdateProduct(Product product)
            {
                _context.Products.Update(product);
            }

            public void DeleteProduct(Product product)
            {
                _context.Products.Remove(product);
            }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
}
