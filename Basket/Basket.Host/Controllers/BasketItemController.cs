using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;
using System.Security.Claims;
using Infrastructure;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("basket.basketItem")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketItemController : ControllerBase
    {
        private List<string> _items = new List<string>();

        [HttpPost(Name = "AddBasketItem")]
        public IActionResult AddBasketItem(string item)
        {
            _items.Add(item);
            return Ok();
        }
    }
}
