using EcommerceMvcDbFirstDemo.Repositories.Interfaces;
using EcommerceMvcDbFirstDemo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMvcDbFirstDemo.Services.Implementation
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<SelectList> GetCategorySelectListAsync(int? selectedCategoryId = null)
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync();

            return new SelectList(
                categories,
                "CategoryId",
                "CategoryName",
                selectedCategoryId
            );
        }
    }
}
