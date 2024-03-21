using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task TestAdd(string userId, string data);
    Task<TestGetResponse> TestGet(string userId);
    public Task AddItemToBasketAsync<T>(string userId, T data);
    public Task<bool> DeleteItemBasketAsync<T>(string userId, T data);
    public Task<List<T>> GetItemsBasketAsync<T>(string userId);
}