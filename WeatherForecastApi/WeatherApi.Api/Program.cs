using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Application.Services;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Data;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using WeatherForecastApi.WeatherApi.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(provider =>
    new SqlConnection(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IFavoriteCityRepository, FavoriteCityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IFavoriteCityService, FavoriteCityService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Register HttpClient for external API requests
builder.Services.AddHttpClient("OpenWeatherMap", client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();