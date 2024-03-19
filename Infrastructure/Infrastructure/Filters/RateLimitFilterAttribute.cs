using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Filters
{
    public class RateLimitFilter : ActionFilterAttribute
    {
        private readonly int _limit;

        public RateLimitFilter(int limit)
        {
            _limit = limit;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            var serviceProvider = context.HttpContext.RequestServices;
            var endpoint = context.HttpContext.GetEndpoint();
            var key = $"{endpoint}:{ipAddress}";

            try
            {
                var redis = serviceProvider.GetRequiredService<IRedisCacheConnectionService>().Connection;

                var database = redis.GetDatabase();

                var currentCount = await database.StringIncrementAsync(key);

                if(currentCount == 1)
                {
                    await database.KeyExpireAsync(key, TimeSpan.FromMinutes(10));
                }

                if(currentCount > _limit)
                {
                    context.Result = new StatusCodeResult(429);
                    return;
                }
            }
            catch (Exception) 
            {
                context.Result = new StatusCodeResult(500);
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
