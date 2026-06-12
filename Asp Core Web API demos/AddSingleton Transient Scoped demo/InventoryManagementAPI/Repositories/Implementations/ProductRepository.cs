using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

         public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
           return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
           return await _context.Products.FindAsync(id);
        }
    }
}
