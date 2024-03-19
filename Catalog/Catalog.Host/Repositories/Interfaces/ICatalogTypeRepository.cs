using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<PaginatedItems<CatalogType>> GetByPageAsync(int pageIndex, int pageSize);
        Task<int?> Add(string type);
        Task<bool> Update(int id, string type);
        Task<bool> Delete(int id);
        Task<List<int>> GetBrandIds();
    }
}
