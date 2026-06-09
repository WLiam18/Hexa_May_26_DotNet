using ProductAPI.Repositories.Implementations;
using ProductAPI.Repositories.Interfaces;
using ProductAPI.Services.Implementations;
using ProductAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository (DI: when someone asks IProductRepository, give ProductRepository)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register Service (DI: when someone asks IProductService, give ProductService)
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();