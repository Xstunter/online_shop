using Basket.Host.Models;
using Basket.Host.Repositories.Interfaces;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task AddItemToBasketAsync<T>(string userId, T data)
    {
        await _basketRepository.AddItemToBasketAsync(userId, data);
    }

    public async Task<bool> DeleteItemBasketAsync<T>(string userId, T data)
    {
        return await _basketRepository.DeleteItemBasketAsync(userId, data);
    }

    public async Task<List<T>> GetItemsBasketAsync<T>(string userId)
    {
        return await _basketRepository.GetItemsBasketAsync<T>(userId);
    }

    public Task TestAdd(string userId, string data)
    {
        throw new NotImplementedException();
    }

    public Task<TestGetResponse> TestGet(string userId)
    {
        throw new NotImplementedException();
    }
}