using InventoryManagementAPI.Dtos;

namespace InventoryManagementAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();

        Task<ProductResponseDto?> GetProductByIdAsync(int productId);

        Task<ProductResponseDto> AddProductAsync(ProductCreateDto productCreateDto);
    }
}
