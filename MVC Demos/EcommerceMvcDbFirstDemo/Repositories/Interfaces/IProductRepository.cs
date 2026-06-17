using EcommerceMvcDbFirstDemo.Models;

namespace EcommerceMvcDbFirstDemo.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int productId);

        Task<Product?> GetProductByIdWithCategoryAsync(int productId);

        Task AddProductAsync(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        Task SaveChangesAsync();

    }
}
