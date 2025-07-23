using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuoteKeeper.API.Config;
using QuoteKeeper.API.Data;
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Extensions;
using QuoteKeeper.API.Services;
using QuoteKeeper.API.Middleware;
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
//builder.Services.AddAuthorization();
builder.Services.AddAuthorizationPolicies();

builder.Services.AddScoped<AuthService>();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


//builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddScoped<IBookService, BookService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CustomUnauthorizedMiddleware>();
app.MapControllers();


app.Run();

public partial class Program { }
