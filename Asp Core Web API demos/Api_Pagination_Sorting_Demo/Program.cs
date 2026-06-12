
using Api_Pagination_Sorting_Demo.Data;
using Api_Pagination_Sorting_Demo.Repository.Implementations;
using Api_Pagination_Sorting_Demo.Repository.Interfaces;
using Api_Pagination_Sorting_Demo.Services.Implementations;
using Api_Pagination_Sorting_Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Pagination_Sorting_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
          
            builder.Services.AddDbContext<HospitalAppointmentDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
