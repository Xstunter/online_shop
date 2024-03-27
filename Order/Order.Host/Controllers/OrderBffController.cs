using Infrastructure.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Services.Interfaces;
using System.Net;
using Order.Host.Models.Requests;
using Order.Host.Models.Dtos;
using Order.Host.Models.Response;
using Order.Host.Models;

namespace Order.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private IOrderService _orderService;
        private ILogger<OrderBffController> _logger;
        public OrderBffController(
            ILogger<OrderBffController> logger,
            IOrderService orderService)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedOrdersResponse<OrderHistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllClientOrders([FromBody] OrdersRequest request)
        {
            var result = await _orderService.GetAllClientOrdersAsync(request.ClientId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaginatedOrderResponse<OrderHistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GerOrder(OrderRequest request)
        {
            var result = await _orderService.GetClientOrderAsync(request.Id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
