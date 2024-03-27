using Basket.Host.Models;
using Basket.Host.Models.Dtos;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    public Task AddItemToBasketAsync(string userId, BasketItemDataDto data);
    public Task<bool> DeleteItemBasketAsync(string userId, int id);
    public Task<List<T>> GetItemsBasketAsync<T>(string userId);
}