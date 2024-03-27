using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize);
        Task<int?> AddAsync(string brand);
        Task<bool> UpdateAsync(int id, string brand);
        Task<bool> DeleteAsync(int id);
        Task<List<int>> GetBrandIdsAsync();
    }
}
