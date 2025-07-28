using QuoteKeeper.API.Middleware;
namespace QuoteKeeper.API.Middleware
{
    public class CustomUnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomUnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\":\"You must log in to perform this action.\"}");
            }
        }
    }
}
