using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuoteKeeper.API.Config;
using QuoteKeeper.API.Data;
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Config;
using QuoteKeeper.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Jwt Settings (in appsettings.Development.json)
builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .ValidateDataAnnotations()
    .Validate(config =>
    {
        return !string.IsNullOrEmpty(config.Key) &&
               config.ExpiresInMinutes > 0;
    }, "JwtSettings validation failed.");

    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    builder.Services.AddJwtAuthentication(jwtSettings);

builder.Services.AddCorsPolicy();
builder.Services.AddPasswordHasher();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowFrontend");

app.Run();

public partial class Program { }
