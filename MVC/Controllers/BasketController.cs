using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
using System.Runtime.CompilerServices;

namespace MVC.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> AddToBasket(int id)
    {
        await _basketService.AddItemBasket(id);
        return RedirectToAction("Index", "Catalog");
    }
    public async Task<IActionResult> Index()
    {
       
        return View();
    }
}