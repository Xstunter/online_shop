using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;
        public CatalogTypeRepository(
            ApplicationDbContext context,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<int?> Add(string type)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Type = type
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var item = _dbContext.CatalogTypes.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found type with id:{id}!");
                }

                _dbContext.CatalogTypes.Remove(item);

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
                var typeIds = await _dbContext.CatalogTypes
                    .Select(t => t.Id)
                    .ToListAsync();

                if (typeIds == null)
                {
                    throw new ArgumentNullException($"Type's ids not found");
                }

                return typeIds;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new List<int>();
            }
        }

        public async Task<PaginatedItems<CatalogType>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogTypes
            .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogTypes
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogType>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<bool> Update(int id, string type)
        {
            try
            {
                var item = _dbContext.CatalogTypes.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found type with id:{id}!");
                }

                item.Type = type;

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
