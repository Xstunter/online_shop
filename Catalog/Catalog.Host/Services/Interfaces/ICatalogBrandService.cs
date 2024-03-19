namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(string brand);
        Task<bool> Update(int id, string brand);
        Task<bool> Delete(int id);
        Task<List<int>> GetBrandIds();
    }
}
