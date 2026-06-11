using OrderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services with different lifetimes
builder.Services.AddTransient<ITransientGuid, TransientGuid>();
builder.Services.AddScoped<IScopedGuid, ScopedGuid>();
builder.Services.AddSingleton<ISingletonGuid, SingletonGuid>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();