using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<PaginatedItems<CatalogType>> GetByPageAsync(int pageIndex, int pageSize);
        Task<int?> AddAsync(string type);
        Task<bool> UpdateAsync(int id, string type);
        Task<bool> DeleteAsync(int id);
        Task<List<int>> GetBrandIdsAsync();
    }
}
