using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels.Basket;
using MVC.ViewModels.Catalog;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IHttpClientService httpClient,
            ILogger<BasketService> logger,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = settings;
        }
        public async Task AddItemBasket(int id)
        {
            await _httpClient.SendAsync<object, int>($"{_settings.Value.BasketUrl}/AddBasketItem", HttpMethod.Post, id);
        }

        public async Task DeleteAllItemsBasket()
        {
            await _httpClient.SendAsync<object, object>($"{_settings.Value.BasketUrl}/DeleteAllBasketItems", HttpMethod.Post, null);
        }

        public async Task DeleteItemBasket(int id)
        {
            await _httpClient.SendAsync<object, int>($"{_settings.Value.BasketUrl}/DeleteBasketItem", HttpMethod.Post, id);
        }

        public Task<List<BasketItems>> GetBasketItems()
        {
            return _httpClient.SendAsync<List<BasketItems>, object>($"{_settings.Value.BasketUrl}/GetItemsBasket", HttpMethod.Post, null);
        }
        
    }
}
