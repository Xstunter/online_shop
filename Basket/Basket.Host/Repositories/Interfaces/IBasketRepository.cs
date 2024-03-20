namespace Basket.Host.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task AddOrUpdateItemToBasketAsync<T>(string key, T value);
        public Task<T> GetItemBasketAsync<T>(string key);
        public Task<bool> DeleteItemBasket(string key);
    }
}
