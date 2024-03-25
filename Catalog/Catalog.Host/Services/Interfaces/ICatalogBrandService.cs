namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> AddAsync(string brand);
        Task<bool> UpdateAsync(int id, string brand);
        Task<bool> DeleteAsync(int id);
        Task<List<int>> GetBrandIdsAsync();
    }
}
