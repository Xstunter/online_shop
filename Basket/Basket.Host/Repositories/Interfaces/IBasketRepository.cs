namespace Basket.Host.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task AddItemToBasketAsync<T>(string key, T value);
        public Task<T> GetItemBasketAsync<T>(string key);
        public Task<bool> DeleteItemBasketAsync<T>(string key, T data);
        public Task<List<T>> GetItemsBasketAsync<T>(string key);
    }
}
