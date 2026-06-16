using EcommerceMvcDbFirstDemo.Models;
using EcommerceMvcDbFirstDemo.Repositories.Interfaces;
using EcommerceMvcDbFirstDemo.Services.Interfaces;
using EcommerceMvcDbFirstDemo.ViewModels;

namespace EcommerceMvcDbFirstDemo.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(
            IProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product?> GetProductDetailsAsync(int productId)
        {
            return await _productRepository.GetProductByIdWithCategoryAsync(productId);
        }

        public async Task<ProductCreateViewModel?> GetProductForEditAsync(int productId)
        {
            Product? product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            ProductCreateViewModel model = new ProductCreateViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                ExistingImagePath = product.ImagePath
            };

            return model;
        }

        public async Task CreateProductAsync(ProductCreateViewModel model)
        {
            string? imagePath = null;

            if (model.ProductImage != null && model.ProductImage.Length > 0)
            {
                imagePath = await SaveProductImageAsync(model.ProductImage);
            }

            Product product = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                CategoryId = model.CategoryId,
                ImagePath = imagePath,
                CreatedDate = DateTime.Now
            };

            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductAsync(ProductCreateViewModel model)
        {
            Product? product = await _productRepository.GetProductByIdAsync(model.ProductId);

            if (product == null)
            {
                return false;
            }

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.CategoryId = model.CategoryId;

            if (model.ProductImage != null && model.ProductImage.Length > 0)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    DeleteExistingImage(product.ImagePath);
                }

                product.ImagePath = await SaveProductImageAsync(model.ProductImage);
            }

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }

        public async Task<Product?> GetProductForDeleteAsync(int productId)
        {
            return await _productRepository.GetProductByIdWithCategoryAsync(productId);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            Product? product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                DeleteExistingImage(product.ImagePath);
            }

            _productRepository.DeleteProduct(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }

        private async Task<string> SaveProductImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "products"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString()
                                    + "_"
                                    + Path.GetFileName(imageFile.FileName);

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            string databaseImagePath = "/images/products/" + uniqueFileName;

            return databaseImagePath;
        }

        private void DeleteExistingImage(string imagePath)
        {
            string existingFilePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                imagePath.TrimStart('/')
            );

            if (File.Exists(existingFilePath))
            {
                File.Delete(existingFilePath);
            }
        }
    }
}
