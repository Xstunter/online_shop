using Infrastructure.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Repositories.Interfaces;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Response;
using System.Net;
using Order.Host.Data.Entities;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("order.orderHistory")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;
        private IOrderBasketService _orderBasketService;

        public OrderController(
            IOrderBasketService orderBasketService,
            ILogger<OrderController> logger)
        {
            _logger = logger;
            _orderBasketService = orderBasketService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            var result = await _orderBasketService.AddAsync(request.ClientId, request.Name, request.LastName, request.TotalPrice, request.BasketItems);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequest request)
        {
            
            var result = await _orderBasketService.UpdateAsync(request.Id, request.orderStatus);

            if (result == false)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrder(DeleteOrderRequest request)
        {
            var result = await _orderBasketService.DeleteAsync(request.Id);

            if (result == false)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}