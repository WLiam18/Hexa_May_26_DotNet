using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Repositories.Interfaces;
using ProductAPI.Services.Interfaces;

namespace ProductAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            
            // Convert Model → DTO
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            // Convert Model → DTO
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public async Task<(bool Success, string Message, ProductResponseDto? Data)> CreateAsync(ProductCreateDto dto)
        {
            // Business rule: Price cannot be negative
            if (dto.Price < 0)
                return (false, "Price cannot be negative", null);

            // Business rule: Stock cannot be negative
            if (dto.Stock < 0)
                return (false, "Stock cannot be negative", null);

            // Business rule: Name cannot be empty
            if (string.IsNullOrWhiteSpace(dto.Name))
                return (false, "Product name is required", null);

            // Convert DTO → Model
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _repository.AddAsync(product);

            // Convert Model → Response DTO
            var response = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            return (true, "Product created successfully", response);
        }

        public async Task<(bool Success, string Message, ProductResponseDto? Data)> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return (false, "Product not found", null);

            // Business rules
            if (dto.Price < 0)
                return (false, "Price cannot be negative", null);

            if (dto.Stock < 0)
                return (false, "Stock cannot be negative", null);

            // Update properties
            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;

            await _repository.UpdateAsync(existing);

            // Convert Model → Response DTO
            var response = new ProductResponseDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Price = existing.Price,
                Stock = existing.Stock
            };

            return (true, "Product updated successfully", response);
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return (false, "Product not found");

            await _repository.DeleteAsync(product);
            return (true, "Product deleted successfully");
        }
    }
}