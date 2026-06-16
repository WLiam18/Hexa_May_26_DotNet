using EcommerceMvcDbFirstDemo.Models;
using EcommerceMvcDbFirstDemo.ViewModels;

namespace EcommerceMvcDbFirstDemo.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductDetailsAsync(int productId);

        Task<ProductCreateViewModel?> GetProductForEditAsync(int productId);

        Task CreateProductAsync(ProductCreateViewModel model);

        Task<bool> UpdateProductAsync(ProductCreateViewModel model);

        Task<Product?> GetProductForDeleteAsync(int productId);

        Task<bool> DeleteProductAsync(int productId);
    }
}
