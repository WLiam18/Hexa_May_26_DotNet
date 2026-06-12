using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        //Task UpdateProductAsync(Product product);
        //Task DeleteProductAsync(int id);
    }
}
