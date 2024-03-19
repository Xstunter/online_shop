using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;
        public CatalogBrandRepository(
            ApplicationDbContext context,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<int?> Add(string brand)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = brand
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var item = _dbContext.CatalogBrands.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found brand with id:{id}!");
                }

                _dbContext.CatalogBrands.Remove(item);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task<List<int>> GetBrandIds()
        {
            try
            {
                var brandIds = await _dbContext.CatalogBrands
                    .Select(t => t.Id)
                    .ToListAsync();

                if (brandIds == null)
                {
                    throw new ArgumentNullException($"Brand's ids not found");
                }

                return brandIds;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new List<int>();
            }
        }

        public async Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogBrands
            .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogBrands
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogBrand>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<bool> Update(int id, string brand)
        {
            try
            {
                var item = _dbContext.CatalogBrands.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found brand with id:{id}!");
                }

                item.Brand = brand;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
    }
}
