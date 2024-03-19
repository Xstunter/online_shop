using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _limit;
    public RateLimitMiddleware(RequestDelegate next, int limit)
    {
        _next = next;
        _limit = limit;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var serviceProvider = context.RequestServices;
        var endpoint = context.GetEndpoint();
        var key = $"{endpoint}:{ipAddress}";

        try
        {
            var redis = serviceProvider.GetRequiredService<IRedisCacheConnectionService>().Connection;

            var database = redis.GetDatabase();

            var currentCount = await database.StringIncrementAsync(key);

            if (currentCount == 1)
            {
                await database.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }

            if (currentCount > _limit)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many requests");
                return;
            }
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            return;
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}