using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;
using System.Security.Claims;
using Infrastructure;
using Basket.Host.Services.Interfaces;
using System.Net;
using Basket.Host.Models;
using Infrastructure.Filters;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketBffController : ControllerBase
    {

        private readonly ILogger<BasketBffController> _logger;
        private readonly IBasketService _basketService;


        public BasketBffController(
            ILogger<BasketBffController> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult LogMassege(string massege)
        {

            _logger.LogInformation($"LogMassege: {massege}");

            return Ok();
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetUserId()
        {
            string userId = User.FindFirstValue("sub");

            _logger.LogInformation($"User Id: {userId}");

            return Ok();
        }

        [HttpPost]
        [RateLimitFilter(10)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> TestAdd(TestAddRequest data)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            await _basketService.TestAdd(basketId!, data.Data);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(TestGetResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TestGet()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _basketService.TestGet(basketId!);
            return Ok(response);
        }
    }
}