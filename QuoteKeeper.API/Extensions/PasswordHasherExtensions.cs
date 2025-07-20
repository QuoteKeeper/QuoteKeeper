using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using QuoteKeeper.API.Extensions;
using QuoteKeeper.API.Models;

namespace QuoteKeeper.API.Extensions
{
    public static class PasswordHasherExtensions
    {
        public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
