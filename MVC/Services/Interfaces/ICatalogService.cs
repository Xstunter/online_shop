using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface IBrasketService
{
    Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type);
    Task<IEnumerable<SelectListItem>> GetBrands(int page, int take);
    Task<IEnumerable<SelectListItem>> GetTypes(int page, int take);
}
