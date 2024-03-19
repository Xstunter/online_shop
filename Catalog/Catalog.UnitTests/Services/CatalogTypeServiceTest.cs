using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Host.Repositories;
using FluentAssertions;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;

        private readonly CatalogType _testType = new CatalogType()
        {
            Type = "TestType"
        };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _logger = new Mock<ILogger<CatalogService>>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);
            _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object);
        }

        [Fact]

        public async Task AddTypeAsync_Success()
        {
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Add(_testType.Type);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task AddTypeAsync_Failed()
        {
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Add(_testType.Type);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteTypeAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Delete(testId);

            result.Should().Be(testResult);
        }

        [Fact]

        public async Task DeleteTypeAsync_Failed()
        {
            bool testResult = false;
            int testId = 1000;

            _catalogTypeRepository.Setup(s => s.Delete(
                It.IsAny<int>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Delete(testId);

            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateTypeAsync_Success()
        {
            bool testResult = true;
            int testId = 1;

            var _newTestType = new CatalogType()
            {
                Type = "NewType"
            };

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Update(testId, _newTestType.Type);

            result.Should().Be(testResult);

        }

        [Fact]

        public async Task UpdateTypedAsync_Failed()
        {
            bool testResult = false;
            int testId = 1000;

            var _newTestType = new CatalogType()
            {
                Type = "NewType"
            };

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<int>(),
                It.IsAny<string>())).ReturnsAsync(testResult);

            var result = await _catalogTypeService.Update(testId, _newTestType.Type);

            result.Should().Be(testResult);
        }
    }
}
