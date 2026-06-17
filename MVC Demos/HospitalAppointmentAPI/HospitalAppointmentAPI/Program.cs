using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using HospitalAppointmentAPI.JsonConverters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });

// CORS - allow Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configuration & DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HospitalAppointmentAPI.Data.HospitalAppointmentDbContext>(options =>
    options.UseSqlServer(connectionString));

// JWT Authentication
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSection.GetValue<string>("Secret");
if (!string.IsNullOrEmpty(jwtSecret))
{
    var key = System.Text.Encoding.UTF8.GetBytes(jwtSecret);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

    builder.Services.AddAuthorization();
}

// Repositories
builder.Services.AddScoped<HospitalAppointmentAPI.Repositories.IDoctorRepository, HospitalAppointmentAPI.Repositories.DoctorRepository>();
builder.Services.AddScoped<HospitalAppointmentAPI.Repositories.IPatientRepository, HospitalAppointmentAPI.Repositories.PatientRepository>();
builder.Services.AddScoped<HospitalAppointmentAPI.Repositories.IAppointmentRepository, HospitalAppointmentAPI.Repositories.AppointmentRepository>();
builder.Services.AddScoped<HospitalAppointmentAPI.Repositories.IUserRepository, HospitalAppointmentAPI.Repositories.UserRepository>();

// Services
builder.Services.AddScoped<HospitalAppointmentAPI.Services.IDoctorService, HospitalAppointmentAPI.Services.DoctorService>();
builder.Services.AddScoped<HospitalAppointmentAPI.Services.IPatientService, HospitalAppointmentAPI.Services.PatientService>();
builder.Services.AddScoped<HospitalAppointmentAPI.Services.IAppointmentService, HospitalAppointmentAPI.Services.AppointmentService>();
builder.Services.AddScoped<HospitalAppointmentAPI.Services.IAuthService, HospitalAppointmentAPI.Services.AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token only. Do not type Bearer manually."
    };
    options.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };
    options.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger UI for testing in all environments.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Enable CORS policy
app.UseCors("AllowAngularDev");

app.UseAuthentication();

// Authorization middleware (requires UseAuthentication first)
app.UseAuthorization();

app.MapControllers();

app.Run();
