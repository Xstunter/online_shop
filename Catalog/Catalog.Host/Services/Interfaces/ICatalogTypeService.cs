namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string type);
        Task<bool> Update(int id, string type);
        Task<bool> Delete(int id);
        Task<List<int>> GetTypeIds();
    }
}
