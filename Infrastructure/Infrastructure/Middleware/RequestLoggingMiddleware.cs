using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            { 
                await _next(context);

                _logger.LogInformation($"Received request: {context.Request.Method} Response status: {context.Response.StatusCode} {context.Request.Path}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing request");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
