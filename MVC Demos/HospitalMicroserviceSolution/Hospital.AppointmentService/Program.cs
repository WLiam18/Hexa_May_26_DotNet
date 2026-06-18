
using Hospital.AppointmentService.Data;
using Hospital.AppointmentService.Repositories.Implementations;
using Hospital.AppointmentService.Repositories.Interfaces;
using Hospital.AppointmentService.Services.Implementations;
using Hospital.AppointmentService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hospital.AppointmentService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<AppointmentDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentDbConnection")));

            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService1>();

            builder.Services.AddHttpClient<IDoctorApiClient, DoctorApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:DoctorService"]!);
            });

            builder.Services.AddHttpClient<IPatientApiClient, PatientApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:PatientService"]!);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                string secretKey = builder.Configuration["JwtSettings:SecretKey"]!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey))
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                AppointmentDbContext context = scope.ServiceProvider.GetRequiredService<AppointmentDbContext>();
                await context.Database.MigrateAsync();
            }
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
