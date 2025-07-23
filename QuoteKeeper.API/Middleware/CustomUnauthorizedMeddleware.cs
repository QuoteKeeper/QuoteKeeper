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
            context.Response.OnStarting(async () =>
         {
             if (context.Response.StatusCode == StatusCodes.Status401Unauthorized &&
                 !context.Response.HasStarted)
             {
                 context.Response.ContentType = "application/json";
                 var responseText = "{\"message\":\"You must log in to perform this action.\"}";
                 var bytes = System.Text.Encoding.UTF8.GetBytes(responseText);
                 context.Response.ContentLength = bytes.Length;
                 await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
             }
         });

            await _next(context);
        }
    }
}
