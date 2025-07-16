namespace QuoteKeeper.API.Extensions
{
    public static class UseAuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPplicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));
            });
            return services;
        }
     }
    
}