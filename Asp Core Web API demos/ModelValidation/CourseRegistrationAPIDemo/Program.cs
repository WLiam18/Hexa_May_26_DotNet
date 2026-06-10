using CourseRegistrationAPIDemo.Dtos;
using CourseRegistrationAPIDemo.Services;
using FluentValidation;
using CourseRegistrationAPIDemo.Validations;
using FluentValidation.AspNetCore;
    
namespace CourseRegistrationAPIDemo
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CourseRegistrationCreateDtoValidator>();
                });
        builder.Services.AddScoped<ICourseRegistrationService,CourseRegistrationService>();
         
          
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IValidator<CourseRegistrationCreateDto>, CourseRegistrationCreateDtoValidator>();
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
