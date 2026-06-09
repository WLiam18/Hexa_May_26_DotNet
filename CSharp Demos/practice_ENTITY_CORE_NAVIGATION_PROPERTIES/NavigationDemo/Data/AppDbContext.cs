using Microsoft.EntityFrameworkCore;
using NavigationDemo.Models;

namespace NavigationDemo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=NavigationDemoDB;User Id=sa;Password=StrongPass@123!;TrustServerCertificate=true;");
        }
    }
}