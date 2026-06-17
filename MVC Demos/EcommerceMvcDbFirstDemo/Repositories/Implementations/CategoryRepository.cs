using EcommerceMvcDbFirstDemo.Data;
using EcommerceMvcDbFirstDemo.Models;
using EcommerceMvcDbFirstDemo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcDbFirstDemo.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceMvcDbContext _context;

        public CategoryRepository(EcommerceMvcDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }
    }
}
