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
using System.Net;
using ServiceStack.Web;

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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBasketItem([FromBody] int id)
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

            var firstItem = catalogItem.Data.First();

            var itemData = new BasketItemDataDto
            {
                Id = firstItem.Id,
                Name = firstItem.Name,
                PictureUrl = firstItem.PictureUrl,
                Price = firstItem.Price
            };

            await _basketService.AddItemToBasketAsync<BasketItemDataDto>(userId, itemData);

            return Ok(itemData);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasketItem(int id)
        {
            string userId = User.FindFirstValue("sub");

            var response = await _httpClient.PostAsJsonAsync("http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetByIdItem", new GetByIdItemRequest { Id = id, PageSize = 1 });

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to get catalog item. Status code: {response.StatusCode}");
            }

            var catalogItem = await response.Content.ReadFromJsonAsync<PaginatedItemsResponse<CatalogItemDto>>();

            if (catalogItem == null)
            {
                return NotFound("Catalog item not found.");
            }

            var firstItem = catalogItem.Data.First();

            var itemData = new BasketItemDataDto
            {
                Id = firstItem.Id,
                Name = firstItem.Name,
                PictureUrl = firstItem.PictureUrl,
                Price = firstItem.Price
            };

            await _basketService.DeleteItemBasketAsync<BasketItemDataDto>(userId, itemData);

            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemsBasket()
        {
            string userId = User.FindFirstValue("sub");

            var items = await _basketService.GetItemsBasketAsync<BasketItemDataDto>(userId);

            return Ok(items);
        }
    }
}
