using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Price = 29.99m, Stock = 50 },
            new Product { Id = 3, Name = "Keyboard", Price = 49.99m, Stock = 30 }
        );
    }
}
