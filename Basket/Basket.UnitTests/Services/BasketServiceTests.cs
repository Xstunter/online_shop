using Basket.Host.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Basket.Host.Models.Dtos;
using StackExchange.Redis;
using Infrastructure.Services;

namespace Basket.UnitTests.Services
{
    public class BasketServiceTests
    {
        private readonly ILogger<BasketRepository> _loggerMock;
        private readonly IRedisCacheConnectionService _redisCacheConnectionServiceMock;
        private readonly IOptions<RedisConfig> _configMock;

        public BasketServiceTests()
        {
            _loggerMock = new Mock<ILogger<BasketRepository>>().Object;
            _redisCacheConnectionServiceMock = new Mock<IRedisCacheConnectionService>().Object;
            _configMock = Options.Create(new RedisConfig { CacheTimeout = TimeSpan.FromMinutes(30) });
        }

        [Fact]
        public async Task AddItemToBasketAsync_Success()
        {
            // Arrange
            var key = "testKey";
            var value = new BasketItemDataDto { Id = 1, Name = "Test Item", Amount = 1 };

            // Mocking dependencies
            var redisMock = new Mock<IDatabase>();

            // Setup Redis cache connection service
            var redisCacheConnectionServiceMock = new Mock<IRedisCacheConnectionService>();
            redisCacheConnectionServiceMock.Setup(x => x.Connection.GetDatabase()).Returns(redisMock.Object);

            // Create BasketRepository instance
            var basketRepository = new BasketRepository(
                _loggerMock,
                _redisCacheConnectionServiceMock,
                _configMock,
                new JsonSerializer());

            // Act
            await basketRepository.AddItemToBasketAsync(key, value);

            // Assert
            redisMock.Verify(x => x.ListRightPushAsync(
                key,
                It.IsAny<RedisValue>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()), Times.Once);
        }
    }   
}
