using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "John", CustomerType = "Regular" },
                new Customer { CustomerId = 2, CustomerName = "Jane", CustomerType = "Premium" },
                new Customer { CustomerId = 3, CustomerName = "Bob", CustomerType = "VIP" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Laptop", UnitPrice = 50000, StockQuantity = 10 },
                new Product { ProductId = 2, ProductName = "Mouse", UnitPrice = 500, StockQuantity = 50 },
                new Product { ProductId = 3, ProductName = "Keyboard", UnitPrice = 1500, StockQuantity = 30 }
            );
        }
    }
}