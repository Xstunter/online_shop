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

            var result = await _httpClient.SendAsync<BasketItems, int>($"{_settings.Value.BasketUrl}/AddBasketItem", HttpMethod.Post, id);


        }

        public Task DeleteItemBasket(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BasketItems> GetBasketItems()
        {
            throw new NotImplementedException();
        }
    }
}
