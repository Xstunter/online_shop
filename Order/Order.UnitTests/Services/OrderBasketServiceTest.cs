using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Data.Enums;
using Order.Host.Models.Requests;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services
{
    public class OrderBasketServiceTest
    {
        private readonly IOrderBasketService _orderBasketService;

        private readonly Mock<IOrderBasketRepository> _orderBasketRepository;
        private readonly Mock<ILogger<OrderBasketService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;

        private readonly AddOrderRequest _testOrder = new AddOrderRequest()
        {
            ClientId = "12345",
            Name = "TestName",
            LastName = "TestLastName",
            TotalPrice = 100,
            BasketItems = new List<AddItemsOrderRequest>()
        };

        public OrderBasketServiceTest()
        {
            _orderBasketRepository = new Mock<IOrderBasketRepository>();
            _logger = new Mock<ILogger<OrderBasketService>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            _orderBasketService = new OrderBasketService(_dbContextWrapper.Object, _logger.Object, _orderBasketRepository.Object);
        }
        [Fact]

        public async Task AddAsync_Success()
        {
            var testResult = 1;

            _orderBasketRepository.Setup(s => s.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<List<AddItemsOrderRequest>>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.AddAsync(_testOrder.ClientId, _testOrder.Name, _testOrder.LastName, _testOrder.TotalPrice, _testOrder.BasketItems);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task AddAsync_Failed()
        {
            int? testResult = null;

            _orderBasketRepository.Setup(s => s.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<List<AddItemsOrderRequest>>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.AddAsync(_testOrder.ClientId, _testOrder.Name, _testOrder.LastName, _testOrder.TotalPrice, _testOrder.BasketItems);

            result.Should().Be(testResult);
        }
        [Fact]

        public async Task DeleteAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            _orderBasketRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.DeleteAsync(testId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task DeleteAsync_Failed()
        {
            bool testResult = false;
            int testId = 1;

            _orderBasketRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.DeleteAsync(testId);

            result.Should().Be(testResult);
        }
        [Fact]

        public async Task UpdateAsync_Success()
        {
            bool testResult = true;
            int testId = 1;
            OrderStatus testStatus = OrderStatus.Cancelled;

            _orderBasketRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<OrderStatus>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.UpdateAsync(testId, testStatus);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task UpdateAsync_Failed()
        {
            bool testResult = false;
            int testId = 1;
            OrderStatus testStatus = OrderStatus.Cancelled;

            _orderBasketRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<OrderStatus>())).ReturnsAsync(testResult);

            var result = await _orderBasketService.UpdateAsync(testId, testStatus);

            result.Should().Be(testResult);
        }
    }
}
