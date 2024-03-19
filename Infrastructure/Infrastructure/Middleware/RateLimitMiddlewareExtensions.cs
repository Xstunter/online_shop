using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Middleware
{
    public static class RateLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder, int limit)
        {
            return builder.UseMiddleware<RateLimitMiddleware>(limit);
        }
    }
}
