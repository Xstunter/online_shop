using Basket.Host.Repositories.Interfaces;
using Basket.Host.Services.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public Task AddItemToBasketAsync<T>(string key, T value) => AddItemToBasketInternalAsync(key, value);

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
        private string GetItemCacheKey(string userId) =>
            $"{userId}";

        private async Task AddItemToBasketInternalAsync<T>(string key, T value,
    IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);
            var serialized = JsonConvert.SerializeObject(value);

            await redis.ListRightPushAsync(cacheKey, serialized);

            await redis.KeyExpireAsync(cacheKey, expiry);

            _logger.LogInformation($"Cached value for key {key} cached");
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
        public async Task<bool> DeleteItemBasketAsync<T>(string key, T value)
        {
            var redis = GetRedisDatabase();

            if (await redis.KeyExistsAsync(key))
            {
                var cacheKey = GetItemCacheKey(key);

                var serialized = _jsonSerializer.Serialize(value);

                await redis.ListRemoveAsync(key, serialized);

                return true;
            }
            return false;
        }

        public async Task<List<T>> GetItemsBasketAsync<T>(string key)
        {
            var redis = GetRedisDatabase();
            

            if (await redis.KeyExistsAsync(key))
            {
                var listItems = await redis.ListRangeAsync(key);
                var items = new List<T>();

                foreach (var serializedItem in listItems)
                {
                    var item = JsonConvert.DeserializeObject<T>(serializedItem);
                    items.Add(item);
                }
                return items;
            }
            return new List<T>();
        }
    }
}
