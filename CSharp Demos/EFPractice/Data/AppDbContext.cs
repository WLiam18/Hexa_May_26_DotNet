using Microsoft.EntityFrameworkCore;
using EFPractice.Models;

namespace EFPractice.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EFPracticeDB;User Id=sa;Password=StrongPass@123!;TrustServerCertificate=true;");
        }
    }
}