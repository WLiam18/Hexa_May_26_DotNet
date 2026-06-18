
using Hospital.AuthService.Data;
using Hospital.AuthService.Models;
using Hospital.AuthService.Repositories.Implementations;
using Hospital.AuthService.Repositories.Interfaces;
using Hospital.AuthService.Services.Implementations;
using Hospital.AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.AuthService
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnection")));

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService1>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await SeedAuthDataAsync(app);
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
            Console.WriteLine("admin:");
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin123"));

            Console.WriteLine("doctor:");
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("doctor123"));

            Console.WriteLine("user:");
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("user123"));
            static async Task SeedAuthDataAsync(WebApplication app)
            {
                using IServiceScope scope = app.Services.CreateScope();

                AuthDbContext context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

                await context.Database.MigrateAsync();

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { RoleName = "Admin" },
                        new Role { RoleName = "Doctor" },
                        new Role { RoleName = "User" }
                    );

                    await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {
                    User adminUser = new User
                    {
                        Username = "admin",
                        FullName = "System Admin",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        IsActive = true
                    };

                    User doctorUser = new User
                    {
                        Username = "doctor",
                        FullName = "Doctor User",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("doctor123"),
                        IsActive = true
                    };

                    User normalUser = new User
                    {
                        Username = "user",
                        FullName = "Normal User",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        IsActive = true
                    };

                    context.Users.AddRange(adminUser, doctorUser, normalUser);
                    await context.SaveChangesAsync();

                    Role adminRole = context.Roles.First(r => r.RoleName == "Admin");
                    Role doctorRole = context.Roles.First(r => r.RoleName == "Doctor");
                    Role userRole = context.Roles.First(r => r.RoleName == "User");

                    context.UserRoles.AddRange(
                        new UserRole { UserId = adminUser.UserId, RoleId = adminRole.RoleId },
                        new UserRole { UserId = doctorUser.UserId, RoleId = doctorRole.RoleId },
                        new UserRole { UserId = normalUser.UserId, RoleId = userRole.RoleId }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

