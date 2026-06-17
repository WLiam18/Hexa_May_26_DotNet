using EcommerceMvcDbFirstDemo.Models;

namespace EcommerceMvcDbFirstDemo.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetActiveCategoriesAsync();
    }
}
