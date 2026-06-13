
using Api_Pagination_Sorting_Demo.Data;
using Api_Pagination_Sorting_Demo.Middlewares;
using Api_Pagination_Sorting_Demo.Repository.Implementations;
using Api_Pagination_Sorting_Demo.Repository.Interfaces;
using Api_Pagination_Sorting_Demo.Services.Implementations;
using Api_Pagination_Sorting_Demo.Services.Interfaces;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Api_Pagination_Sorting_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Admin Password Hash:");
            //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin123"));

            //Console.WriteLine("Doctor Password Hash:");
            //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("doctor123"));

            //Console.WriteLine("Reception Password Hash:");
            //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("reception123"));

            //Console.WriteLine("Patient Password Hash:");
            //Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("patient123"));
            var builder = WebApplication.CreateBuilder(args);

            var logRepository= LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // Add services to the container.
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddLog4Net("log4net.config");

         

            builder.Services.AddControllers();
          
            builder.Services.AddDbContext<HospitalAppointmentDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenService,JwtTokenService>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 string issuer = builder.Configuration["JwtSettings:Issuer"]!;
                 string audience = builder.Configuration["JwtSettings:Audience"]!;
                 string secretKey = builder.Configuration["JwtSettings:SecretKey"]!;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = issuer,

                     ValidateAudience = true,
                     ValidAudience = audience,

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };

             });
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token only. Do not type Bearer manually."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
