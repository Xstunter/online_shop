using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.Controllers;

public class BasketController : Controller
{
    private readonly IBrasketService _basketService;

    public BasketController(IBrasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> Index()
    {
        throw new NotImplementedException();
    }
}