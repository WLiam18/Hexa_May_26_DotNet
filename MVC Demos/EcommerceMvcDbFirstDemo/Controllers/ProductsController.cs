using EcommerceMvcDbFirstDemo.Services.Interfaces;
using EcommerceMvcDbFirstDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMvcDbFirstDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productservice;
        private readonly ICategoryService _categoryservice;
        public ProductsController(IProductService productservice, ICategoryService categoryservice)
        {
            _productservice = productservice;
            _categoryservice = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productservice.GetAllProductsAsync();
            return View(products);
        }


        public async Task<IActionResult> Details(int id)
        {
            var product=await _productservice.GetProductDetailsAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryservice.GetCategorySelectListAsync();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (ProductCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Categories=await _categoryservice.GetCategorySelectListAsync(model.CategoryId);
                return View(model);
            }

            await _productservice.CreateProductAsync(model);
            TempData["SuccessMessage"] = "Product Created Successfully";

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            ProductCreateViewModel? model=await _productservice.GetProductForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _categoryservice.GetCategorySelectListAsync(model.CategoryId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryservice.GetCategorySelectListAsync(model.CategoryId);
                return View(model);
            }
            bool isUpdated=await _productservice.UpdateProductAsync(model);
           if(!isUpdated) {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Product Updated Successfully";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
          var product = await _productservice.GetProductForDeleteAsync(id);

            if (product == null)
            {
                return NotFound();
            }
       
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isDeleted=await _productservice.DeleteProductAsync(id);

            if(!isDeleted)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Product delted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
