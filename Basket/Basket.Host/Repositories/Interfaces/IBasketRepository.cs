using Basket.Host.Models.Dtos;

namespace Basket.Host.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task AddItemToBasketAsync(string key, BasketItemDataDto value);
        public Task<T> GetItemBasketAsync<T>(string key);
        public Task<bool> DeleteItemBasketAsync(string key, int id);
        public Task<List<T>> GetItemsBasketAsync<T>(string key);
    }
}
