using ProductAPI.Models;
using ProductAPI.Repositories.Interfaces;

namespace ProductAPI.Repositories.Implementations
{
    // Fake database (in-memory list for learning)
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();
        private static int _nextId = 1;

        public Task<List<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.ToList());
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task AddAsync(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            // No action needed - reference already updated
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Product product)
        {
            _products.Remove(product);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
            var exists = _products.Any(p => p.Id == id);
            return Task.FromResult(exists);
        }
    }
}