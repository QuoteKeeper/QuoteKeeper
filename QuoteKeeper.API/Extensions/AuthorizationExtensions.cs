using QuoteKeeper.API.Extensions;
namespace QuoteKeeper.API.Extensions
{
    public static class UseAuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUserPolicy", policy =>
                policy.RequireAuthenticatedUser());
            });
            return services;
        }
    }

}