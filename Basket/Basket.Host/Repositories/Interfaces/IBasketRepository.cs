using Basket.Host.Models.Dtos;

namespace Basket.Host.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task AddItemToBasketAsync(string key, BasketItemDataDto value);
        public Task<bool> DeleteItemBasketAsync(string key, int id);
        public Task<bool> DeleteAllItemsBasketAsync(string userId);
        public Task<List<T>> GetItemsBasketAsync<T>(string key);
    }
}
