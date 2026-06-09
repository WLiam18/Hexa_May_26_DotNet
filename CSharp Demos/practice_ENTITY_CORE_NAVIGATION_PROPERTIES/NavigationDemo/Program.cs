using Microsoft.EntityFrameworkCore;
using NavigationDemo.Data;
using NavigationDemo.Models;

namespace NavigationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var electronics = new Category { CategoryName = "Electronics" };
                var books = new Category { CategoryName = "Books" };

                var products = new List<Product>
                {
                    new Product { ProductName = "Laptop", Price = 999, Category = electronics },
                    new Product { ProductName = "Mouse", Price = 25, Category = electronics },
                    new Product { ProductName = "C# Book", Price = 50, Category = books }
                };

                db.Products.AddRange(products);
                db.SaveChanges();

                Console.WriteLine("=== DEMO 1: Reference Navigation ===");
                var firstProduct = db.Products.First();
                Console.WriteLine($"Product: {firstProduct.ProductName} → Category: {firstProduct.Category.CategoryName}\n");

                Console.WriteLine("=== DEMO 2: Collection Navigation ===");
                var category = db.Categories.First(c => c.CategoryName == "Electronics");
                Console.WriteLine($"Category: {category.CategoryName}");
                Console.WriteLine("  Products:");
                foreach (var p in category.Products)
                {
                    Console.WriteLine($"    - {p.ProductName} (₹{p.Price})");
                }
                Console.WriteLine();

                Console.WriteLine("=== DEMO 3: Include() ===");
                var productsWithCategory = db.Products.Include(p => p.Category).ToList();
                foreach (var p in productsWithCategory)
                {
                    Console.WriteLine($"  {p.ProductName} → {p.Category.CategoryName} (₹{p.Price})");
                }
            }

            Console.ReadLine();
        }
    }
}