using Basket.Host.Repositories.Interfaces;
using Basket.Host.Services.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Host.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ILogger<BasketRepository> _logger;
        private readonly IRedisCacheConnectionService _redisCacheConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RedisConfig _config;

        public BasketRepository(
            ILogger<BasketRepository> logger,
            IRedisCacheConnectionService redisCacheConnectionService,
            IOptions<RedisConfig> config,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _redisCacheConnectionService = redisCacheConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;;
        }
        public Task AddOrUpdateItemToBasketAsync<T>(string key, T value) => AddOrUpdateItemToBasketInternalAsync(key, value);

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
        private string GetItemCacheKey(string userId) =>
            $"{userId}";

        private async Task AddOrUpdateItemToBasketInternalAsync<T>(string key, T value,
            IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);
            var serialized = _jsonSerializer.Serialize(value);

            if (await redis.StringSetAsync(cacheKey, serialized, expiry))
            {
                _logger.LogInformation($"Cached value for key {key} cached");
            }
            else
            {
                _logger.LogInformation($"Cached value for key {key} updated");
            }
        }

        public async Task<T> GetItemBasketAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            return serialized.HasValue ?
                _jsonSerializer.Deserialize<T>(serialized.ToString())
                : default(T)!;
        }
        public Task<bool> DeleteItemBasket(string key)
        {
            throw new NotImplementedException();
        }
    }
}
