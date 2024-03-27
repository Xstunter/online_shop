namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> AddAsync(string type);
        Task<bool> UpdateAsync(int id, string type);
        Task<bool> DeleteAsync(int id);
        Task<List<int>> GetTypeIdsAsync();
    }
}
