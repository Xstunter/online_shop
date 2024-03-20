using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;
using Infrastructure;
using System.Security.Claims;
using Basket.Host.Models.Request;
using Basket.Host.Models.Dtos;
using Basket.Host.Services.Interfaces;
using Newtonsoft.Json;
using Basket.Host.Models.Response;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("basket.basketItem")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketItemController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        public BasketItemController (
            HttpClient httpClient,
            IBasketService basketService)
        {
            _httpClient = httpClient;
            _basketService = basketService;
        }

        [HttpPost(Name = "AddBasketItem")]
        public async Task<IActionResult> AddBasketItem(int id)
        {
            string userId = User.FindFirstValue("sub");

            var response = await _httpClient.PostAsJsonAsync("http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetByIdItem", new GetByIdItemRequest { Id = id, PageSize = 1});

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to get catalog item. Status code: {response.StatusCode}");
            }

            var catalogItem = await response.Content.ReadFromJsonAsync<PaginatedItemsResponse<CatalogItemDto>>();

            if (catalogItem == null)
            {
                return NotFound("Catalog item not found.");
            }

            var firstCatalogItem = catalogItem.Data.FirstOrDefault();
            if (firstCatalogItem == null)
            {
                // Обработка случая, когда данных нет
                return NotFound("Catalog item not found.");
            }

            string itemData = ConvertItemToData(firstCatalogItem);

            await _basketService.AddOrUpdateItemToBasketAsync<CatalogItemDto>(userId, itemData);

            return Ok();
        }

        private string ConvertItemToData(CatalogItemDto catalogItem)
        {
            // Преобразование объекта к нужному формату для сервиса корзины
            return JsonConvert.SerializeObject(catalogItem);
        }
    }
}
