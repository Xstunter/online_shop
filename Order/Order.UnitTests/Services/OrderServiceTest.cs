using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Response;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTests.Services
{
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;

        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ILogger<OrderService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<IMapper> _mapper;

        private readonly OrderHistoryDto _testOrder = new OrderHistoryDto()
        {
            ClientId = "12345",
            Name = "TestName",
            LastName = "TestLastName",
            TotalPrice = 100,
            BasketItems = new List<BasketItemDto>()
        };

        public OrderServiceTest()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _logger = new Mock<ILogger<OrderService>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            _orderService = new OrderService(_dbContextWrapper.Object, _logger.Object, _orderRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetAllClientOrdersAsync_Success()
        {
            var testCount = 1;
            string testClientId = "12345";

            var pagingPaginatedItemSuccess = new PaginatedOrder<OrderHistory>()
            {
                Data = new List<OrderHistory>()
                {
                    new OrderHistory
                    {
                        ClientId = "12345"
                    },
                },
                TotalCount = testCount
            };

            var catalogItemSuccess = new OrderHistory()
            {
                ClientId = "12345"
            };

            var catalogItemDtoSuccess = new OrderHistoryDto()
            {
                ClientId = "12345"
            };

            _orderRepository.Setup(s => s.GetAllClientOrdersAsync(
                It.Is<string>(i => i == testClientId))).ReturnsAsync(pagingPaginatedItemSuccess);

            _mapper.Setup(s => s.Map<OrderHistoryDto>(
                It.Is<OrderHistory>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var result = await _orderService.GetAllClientOrdersAsync(testClientId);

            result.Should().NotBeNull();
            result?.Data.Should().NotBeNull();
            result?.Count.Should().Be(testCount);
        }

        [Fact]

        public async Task GetAllClientOrdersAsync_Failed()
        {
            string testClientId = "12345";

            _orderRepository.Setup(s => s.GetAllClientOrdersAsync(
                It.Is<string>(i => i == testClientId))).Returns((Func<PaginatedOrder<OrderHistoryDto>>)null!);

            var result = await _orderService.GetAllClientOrdersAsync(testClientId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetClientOrderAsync_Success()
        {
            var testCount = 1;
            int testId = 1;

            var pagingPaginatedItemSuccess = new PaginatedOrder<OrderHistory>()
            {
                Data = new List<OrderHistory>()
                {
                    new OrderHistory
                    {
                        Id = 1
                    },
                },
                TotalCount = testCount
            };

            var catalogItemSuccess = new OrderHistory()
            {
                Id = 1
            };

            var catalogItemDtoSuccess = new OrderHistoryDto()
            {
                Id = 1
            };

            _orderRepository.Setup(s => s.GetClientOrderAsync(
                It.Is<int>(i => i == testId))).ReturnsAsync(pagingPaginatedItemSuccess);

            _mapper.Setup(s => s.Map<OrderHistoryDto>(
                It.Is<OrderHistory>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var result = await _orderService.GetClientOrderAsync(testId);

            result.Should().NotBeNull();
            result?.Data.Should().NotBeNull();
        }

        [Fact]

        public async Task GetClientOrderAsync_Failed()
        {
            int testId = 1;

            _orderRepository.Setup(s => s.GetClientOrderAsync(
                It.Is<int>(i => i == testId))).Returns((Func<PaginatedOrder<OrderHistoryDto>>)null!);

            var result = await _orderService.GetClientOrderAsync(testId);

            result.Should().BeNull();
        }
    }
}
