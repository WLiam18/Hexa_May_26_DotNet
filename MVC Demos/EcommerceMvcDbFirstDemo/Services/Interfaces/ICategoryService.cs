using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMvcDbFirstDemo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<SelectList> GetCategorySelectListAsync(int? selectedCategoryId = null);
    }
}
