using ProductAPI.DTOs;

namespace ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<(bool Success, string Message, ProductResponseDto? Data)> CreateAsync(ProductCreateDto dto);
        Task<(bool Success, string Message, ProductResponseDto? Data)> UpdateAsync(int id, ProductUpdateDto dto);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}