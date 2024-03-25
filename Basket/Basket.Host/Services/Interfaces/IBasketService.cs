using Basket.Host.Models;
using Basket.Host.Models.Dtos;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task TestAdd(string userId, string data);
    Task<TestGetResponse> TestGet(string userId);
    public Task AddItemToBasketAsync(string userId, BasketItemDataDto data);
    public Task<bool> DeleteItemBasketAsync(string userId, int id);
    public Task<List<T>> GetItemsBasketAsync<T>(string userId);
}