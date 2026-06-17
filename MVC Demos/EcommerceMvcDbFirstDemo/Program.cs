using Microsoft.EntityFrameworkCore;
using EcommerceMvcDbFirstDemo.Data;
using EcommerceMvcDbFirstDemo.Repositories.Interfaces;
using EcommerceMvcDbFirstDemo.Repositories.Implementations;
using EcommerceMvcDbFirstDemo.Services.Interfaces;
using EcommerceMvcDbFirstDemo.Services.Implementation;

namespace EcommerceMvcDbFirstDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<EcommerceMvcDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceDbConnection"));
            });

            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
           builder.Services.AddScoped<ICategoryService,CategoryService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
