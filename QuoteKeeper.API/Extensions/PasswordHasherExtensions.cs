using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace QuoteKeeper.API.Extensions
{
    public static class PasswordHasherExtensions
    {
        public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            return services;
        }
    }
}
