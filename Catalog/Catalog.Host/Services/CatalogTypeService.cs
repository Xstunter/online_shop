using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public Task<int?> AddAsync(string type)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.AddAsync(type));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.DeleteAsync(id));
        }

        public Task<List<int>> GetTypeIdsAsync()
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.GetBrandIdsAsync());
        }

        public Task<bool> UpdateAsync(int id, string type)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.UpdateAsync(id, type));
        }
    }
}
