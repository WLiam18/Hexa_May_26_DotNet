using InventoryManagementAPI.Dtos;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Repositories.Interfaces;
using InventoryManagementAPI.Services.Interfaces;

namespace InventoryManagementAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRequestTrackingService _requestTrackingService;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IApplicationSettingsService _applicationSettingsService;
        public ProductService(IProductRepository productRepository,
            IRequestTrackingService requestTrackingService, 
            IEmailNotificationService emailNotificationService,
            IApplicationSettingsService applicationSettingsService)
        {
            _productRepository = productRepository;
            _requestTrackingService = requestTrackingService;
            _emailNotificationService = emailNotificationService;
            _applicationSettingsService = applicationSettingsService;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto productCreateDto)

        {
            var product =new Product
            {
                ProductName = productCreateDto.ProductName,
                StockQunatity = productCreateDto.StockQunatity,
                UnitPrice = productCreateDto.UnitPrice
            };
            await _productRepository.CreateProductAsync(product);
            if(product.StockQunatity < 10)
            {
                 _emailNotificationService.SendLowStockNotification(
                    product.ProductName, product.StockQunatity);
            }
            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                StockQunatity = product.StockQunatity,
                UnitPrice = product.UnitPrice,
                RequestId = _requestTrackingService.GenerateRequestId(),
                CompanyName = _applicationSettingsService.GetCompanyName()
            };
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
          var products = await _productRepository.GetAllProductsAsync();

            return products.Select(product =>
             new ProductResponseDto
             {
                 ProductId = product.ProductId,
                 ProductName = product.ProductName,
                 StockQunatity = product.StockQunatity,
                 UnitPrice = product.UnitPrice,
                 RequestId = _requestTrackingService.GenerateRequestId(),
                 CompanyName = _applicationSettingsService.GetCompanyName()
             }).ToList();
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int productId)
        {
            var product=await _productRepository.GetProductByIdAsync(productId);
            if(product == null)
            {
                return null;
            }
            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                StockQunatity = product.StockQunatity,
                UnitPrice = product.UnitPrice,
                RequestId = _requestTrackingService.GenerateRequestId(),
                CompanyName = _applicationSettingsService.GetCompanyName()
            };
        }
        }
    }

