using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using FluentAssertions;

namespace Catalog.UnitTests.Services
{
    public class CatalogItemServiceTest
    {
        private readonly ICatalogItemService _catalogItemService;

        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;

        private readonly CatalogItem _testItem = new CatalogItem()
        {
            Name = "Test",
            Description = "Test",
            Price = 100,
            AvailableStock = 100,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            PictureFileName = "Test.png"
        };

        public CatalogItemServiceTest()
        {
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _logger = new Mock<ILogger<CatalogService>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            _catalogItemService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
        }

        [Fact]

        public async Task AddItemAsync_Success()
        {
            var testResult = 1;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Add(_testItem.Name, _testItem.Description,
                                                       _testItem.Price, _testItem.AvailableStock,
                                                       _testItem.CatalogBrandId, _testItem.CatalogTypeId,
                                                       _testItem.PictureFileName);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task AddItemAsync_Failed()
        {
            int? testResult = null;

            _catalogItemRepository.Setup(s => s.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Add(_testItem.Name, _testItem.Description,
                                                       _testItem.Price, _testItem.AvailableStock,
                                                       _testItem.CatalogBrandId, _testItem.CatalogTypeId,
                                                       _testItem.PictureFileName);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task DeleteItemAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            _catalogItemRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Delete(testId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task DeleteItemAsync_Failed()
        {
            bool testResult = false;
            int testId = 1000;

            _catalogItemRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Delete(testId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task UpdateItemAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            var _newTestItem = new CatalogItem()
            {
                Name = "NewTest",
                Description = "NewTest",
                Price = 110,
                AvailableStock = 110,
                CatalogBrandId = 2,
                CatalogTypeId = 2,
                PictureFileName = "NewTest.png"
            };

            _catalogItemRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Update(testId, _newTestItem.Name, _newTestItem.Description,
                                                 _newTestItem.Price, _newTestItem.AvailableStock,
                                                 _newTestItem.CatalogBrandId, _newTestItem.CatalogTypeId,
                                                 _newTestItem.PictureFileName);
            result.Should().Be(testResult);
        }

        [Fact]

        public async Task UpdateItemAsync_Failed()
        {
            bool testResult = false;
            int testId = 1000;

            var _newTestItem = new CatalogItem()
            {
                Name = "NewTest",
                Description = "NewTest",
                Price = 110,
                AvailableStock = 110,
                CatalogBrandId = 2,
                CatalogTypeId = 2,
                PictureFileName = "NewTest.png"
            };

            _catalogItemRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogItemService.Update(testId, _newTestItem.Name, _newTestItem.Description,
                                                 _newTestItem.Price, _newTestItem.AvailableStock,
                                                 _newTestItem.CatalogBrandId, _newTestItem.CatalogTypeId,
                                                 _newTestItem.PictureFileName);

            result.Should().Be(testResult);
        }
    }
}
