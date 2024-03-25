using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetByIdAsync(int pageIndex, int pageSize, int id);
    Task<PaginatedItems<CatalogItem>> GetByBrandAsync(int pageIndex, int pageSize, string brand);
    Task<PaginatedItems<CatalogItem>> GetByTypeAsync(int pageIndex, int pageSize, string type);
    Task<PaginatedItems<CatalogItem>> GetByFilterAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> UpdateAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> DeleteAsync(int id);
}