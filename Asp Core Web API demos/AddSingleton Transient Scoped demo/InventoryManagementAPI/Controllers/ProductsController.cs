using InventoryManagementAPI.Dtos;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRequestTrackingService _requestTrackingService;
        private readonly IApplicationSettingsService _applicationSettingsService;

        public ProductsController(IProductService productService, IRequestTrackingService requestTrackingService, IApplicationSettingsService applicationSettingsService)
        {
            _productService = productService;
            _requestTrackingService = requestTrackingService;
            _applicationSettingsService = applicationSettingsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new
            {
                StatusCode= StatusCodes.Status200OK,
                Message = "Products retrieved successfully",
                RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                ComplanyName= _applicationSettingsService.GetCompanyName(),
                Data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if(id<=0)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid product ID",
                    //RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                    //ComplanyName = _applicationSettingsService.GetCompanyName()
                });
            }
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Product not found",
                    //RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                    //ComplanyName = _applicationSettingsService.GetCompanyName()
                });
            }
            return Ok(                
                new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Products retrieved successfully",
                    //RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                    //ComplanyName = _applicationSettingsService.GetCompanyName(),
                    Data = product
                });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null || string.IsNullOrEmpty(productCreateDto.ProductName) || productCreateDto.StockQunatity < 0 || productCreateDto.UnitPrice < 0)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid product data",
                    //RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                    //ComplanyName = _applicationSettingsService.GetCompanyName()
                });
            }
            var createdProduct = await _productService.AddProductAsync(productCreateDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, new
            {
                StatusCode = StatusCodes.Status201Created,
                Message = "Product created successfully",
                //RequestIdFromController = _requestTrackingService.GenerateRequestId(),
                //ComplanyName = _applicationSettingsService.GetCompanyName(),
                Data = createdProduct
            });
        }   
    }
}
