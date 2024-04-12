using MVC.Services.Interfaces;
using MVC.ViewModels.Catalog;
using MVC.ViewModels.BasketViewModels;
using Infrastructure.Identity;
using Infrastructure;

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
    public async Task<IActionResult> DeleteItemBasket(int id)
    {
        await _basketService.DeleteItemBasket(id);
        return RedirectToAction("Index", "Basket");
    }
    public async Task<IActionResult> Index()
    {
        var basket = await _basketService.GetBasketItems();

        var vm = new IndexViewModel()
        {
            BasketItems = basket
        };

        return View(vm);
    }
}