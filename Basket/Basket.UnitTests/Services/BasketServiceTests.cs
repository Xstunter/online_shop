using Basket.Host.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StackExchange.Redis;
using Basket.Host.Models.Dtos;
using Newtonsoft.Json;
using FluentAssertions;

namespace Basket.UnitTests.Services
{
    public class BasketServiceTests
    {
        private readonly BasketRepository _basketRepository;

        private readonly Mock<IOptions<RedisConfig>> _config;
        private readonly Mock<ILogger<BasketRepository>> _logger;
        private readonly Mock<IRedisCacheConnectionService> _redisCacheConnectionService;
        private readonly Mock<IJsonSerializer> _jsonSerializer;
        private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer;
        private readonly Mock<IDatabase> _redisDataBase;

        private BasketItemDataDto basketItems = new BasketItemDataDto
        {
            Id = 1,
            Name = "TestItem"
        };

        public BasketServiceTests()
        {
            _config = new Mock<IOptions<RedisConfig>>();
            _logger = new Mock<ILogger<BasketRepository>>();
            _redisCacheConnectionService = new Mock<IRedisCacheConnectionService>();
            _connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            _redisDataBase = new Mock<IDatabase>();
            _jsonSerializer = new Mock<IJsonSerializer>();

            _config.Setup(x => x.Value).Returns(new RedisConfig() { CacheTimeout = TimeSpan.Zero });

            _connectionMultiplexer
                .Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(_redisDataBase.Object);

            _redisCacheConnectionService
                .Setup(x => x.Connection)
                .Returns(_connectionMultiplexer.Object);

            _basketRepository =
                new BasketRepository(
                    _logger.Object,
                    _redisCacheConnectionService.Object,
                    _config.Object,
                    _jsonSerializer.Object);
        }

        [Fact]
        public async Task AddItemToBasketAsync_Failed()
        {
            // arrange
            string userId = "TestUserId";

            BasketItemDataDto testEntity = new BasketItemDataDto()
            {
                Name = "TestName"
            };

            _redisDataBase.Setup(expression: x => x.ListRightPushAsync(
                    It.IsAny<RedisKey>(),
                    It.IsAny<RedisValue>(),
                    It.IsAny<When>(),
                    It.IsAny<CommandFlags>()))
                .ReturnsAsync(0);

            // act
            await _basketRepository.AddItemToBasketAsync(userId, testEntity);

            // assert
            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Cached value for key {userId} cached")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task AddItemToBasketAsync_Success()
        {
            // arrange
            string userId = "TestUserId";

            BasketItemDataDto testEntity = new BasketItemDataDto()
            {
                Name = "TestName"
            };

            _redisDataBase.Setup(expression: x => x.ListRightPushAsync(
                    It.IsAny<RedisKey>(),
                    It.IsAny<RedisValue>(),
                    It.IsAny<When>(),
                    It.IsAny<CommandFlags>()))
                .ReturnsAsync(1); // Возвращаемое значение 1 для эмуляции успешной операции

            // act
            await _basketRepository.AddItemToBasketAsync(userId, testEntity);

            // assert
            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Cached value for key {userId} cached")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task DeleteItemBasketAsync_Failed()
        {
            // arrange
            string key = "testKey";

            _redisDataBase.Setup(expression: x => x.KeyExistsAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<CommandFlags>())).ReturnsAsync(false);

            // act
            var result = await _basketRepository.DeleteItemBasketAsync(key, basketItems.Id);

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteItemBasketAsync_Success()
        {
            // arrange
            string key = "testKey";
            int itemIdToRemove = 1; 

            var fakeData = new List<BasketItemDataDto>
            {
                new BasketItemDataDto { Id = 1, Name = "Item 1", Amount = 1 },
                new BasketItemDataDto { Id = 2, Name = "Item 2", Amount = 1 },
                new BasketItemDataDto { Id = 3, Name = "Item 3", Amount = 1 }
            };

            var serializedData = fakeData.Select(item => JsonConvert.SerializeObject(item)).ToArray();

            _redisDataBase.Setup(x => x.KeyExistsAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);
            _redisDataBase.Setup(x => x.ListRangeAsync(It.IsAny<RedisKey>(), 0, It.IsAny<long>(), It.IsAny<CommandFlags>()))
    .Returns(Task.FromResult(serializedData.Select(s => (RedisValue)s).ToArray()));

            // act
            var result = await _basketRepository.DeleteItemBasketAsync(key, itemIdToRemove);

            // assert
            Assert.True(result); 
        }

        [Fact]
        public async Task DeleteAllItemsBasketAsync_Failed()
        {
            // arrange
            string key = "testKey";

            _redisDataBase.Setup(expression: x => x.KeyExistsAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<CommandFlags>())).ReturnsAsync(false);

            // act
            var result = await _basketRepository.DeleteAllItemsBasketAsync(key);

            // assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAllItemsBasketAsync_Success()
        {
            // arrange
            string key = "testKey";
            int itemIdToRemove = 1;

            var fakeData = new List<BasketItemDataDto>
            {
                new BasketItemDataDto { Id = 1, Name = "Item 1", Amount = 1 },
                new BasketItemDataDto { Id = 2, Name = "Item 2", Amount = 1 },
                new BasketItemDataDto { Id = 3, Name = "Item 3", Amount = 1 }
            };

            var serializedData = fakeData.Select(item => JsonConvert.SerializeObject(item)).ToArray();

            _redisDataBase.Setup(x => x.KeyExistsAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);
            _redisDataBase.Setup(x => x.ListRangeAsync(It.IsAny<RedisKey>(), 0, It.IsAny<long>(), It.IsAny<CommandFlags>()))
    .Returns(Task.FromResult(serializedData.Select(s => (RedisValue)s).ToArray()));

            // act
            var result = await _basketRepository.DeleteAllItemsBasketAsync(key);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetItemsBasketAsync_Failed()
        {
            // Arrange
            string key = "nonExistingKey";

            _redisDataBase.Setup(x => x.KeyExistsAsync(key, It.IsAny<CommandFlags>())).ReturnsAsync(false);

            // Act
            var result = await _basketRepository.GetItemsBasketAsync<BasketItemDataDto>(key);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetItemsBasketAsync_Success()
        {
            // Arrange
            string key = "testKey";
            var fakeData = new List<BasketItemDataDto>
            {
                new BasketItemDataDto { Id = 1, Name = "Item 1", Amount = 1 },
                new BasketItemDataDto { Id = 2, Name = "Item 2", Amount = 1 },
                new BasketItemDataDto { Id = 3, Name = "Item 3", Amount = 1 }
            };
            var serializedData = fakeData.Select(item => JsonConvert.SerializeObject(item)).ToArray();

            _redisDataBase.Setup(x => x.KeyExistsAsync(key, It.IsAny<CommandFlags>())).ReturnsAsync(true);
            _redisDataBase.Setup(x => x.ListRangeAsync(key, It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()))
                          .ReturnsAsync(serializedData.Select(s => (RedisValue)s).ToArray());

            // Act
            var result = await _basketRepository.GetItemsBasketAsync<BasketItemDataDto>(key);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(fakeData.Count, result.Count);
            Assert.Equal(fakeData.First().Id, result.First().Id);
            Assert.Equal(fakeData.Last().Id, result.Last().Id);
        }
    }
}   

