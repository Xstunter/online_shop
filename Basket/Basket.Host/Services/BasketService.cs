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

    public async Task AddOrUpdateItemToBasketAsync<T>(string userId, string data)
    {
        await _basketRepository.AddOrUpdateItemToBasketAsync(userId, data);
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