using MVC.ViewModels.Basket;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        public Task AddItemBasket(int id);
        public Task DeleteItemBasket(int id);
        public Task DeleteAllItemsBasket();
        public Task<List<BasketItems>> GetBasketItems();
    }
}
