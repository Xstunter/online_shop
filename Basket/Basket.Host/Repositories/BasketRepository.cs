using Basket.Host.Models.Dtos;
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
        public Task AddItemToBasketAsync(string key, BasketItemDataDto value) => AddItemToBasketInternalAsync(key, value);

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
        private string GetItemCacheKey(string userId) =>
            $"{userId}";

        private async Task AddItemToBasketInternalAsync(string key, BasketItemDataDto value,
    IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);
            var serialized = JsonConvert.SerializeObject(value);

            var existingItems = await redis.ListRangeAsync(cacheKey);
            var existingItem = existingItems.FirstOrDefault(item =>
            {
                var deserializedItem = JsonConvert.DeserializeObject<BasketItemDataDto>(item);
                return deserializedItem.Id == value.Id; // Сравниваем Id айтемов
            });

            if (!string.IsNullOrEmpty(existingItem))
            {
                var deserializedItem = JsonConvert.DeserializeObject<BasketItemDataDto>(existingItem);
                deserializedItem.Amount++;
                var updatedItem = JsonConvert.SerializeObject(deserializedItem);
                await redis.ListRemoveAsync(cacheKey, existingItem);
                await redis.ListRightPushAsync(cacheKey, updatedItem);
            }
            else
            {
                await redis.ListRightPushAsync(cacheKey, serialized);
            }

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
        public async Task<bool> DeleteItemBasketAsync(string key, int id)
        {
            var redis = GetRedisDatabase();

            if (await redis.KeyExistsAsync(key))
            {
                var cacheKey = GetItemCacheKey(key);

                var existingItems = await redis.ListRangeAsync(cacheKey);

                foreach (var existingItem in existingItems)
                {
                    var deserializedItem = JsonConvert.DeserializeObject<BasketItemDataDto>(existingItem);

                    if (deserializedItem.Id == id)
                    {
                        await redis.ListRemoveAsync(cacheKey, existingItem);
                        _logger.LogInformation($"Item with Id {id} removed from basket.");
                        return true;
                    }
                }
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
