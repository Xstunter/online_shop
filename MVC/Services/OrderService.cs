using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels.Order;
using StackExchange.Redis;

namespace MVC.Services
{
    public class OrderService  : IOrderService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IHttpClientService httpClient,
            ILogger<OrderService> logger,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = settings;
        }

        public async Task AddOrderHistory(OrderHistory order)
        {
            await _httpClient.SendAsync<object, OrderHistory>($"{_settings.Value.OrderUrl}Order/AddOrder", HttpMethod.Post, order);
        }

        public async Task<ViewModels.Order.Order> GetOrderHistory(OrderHistoryRequest clientId)
        {
            return await _httpClient.SendAsync<ViewModels.Order.Order, OrderHistoryRequest>($"{_settings.Value.OrderUrl}OrderBff/GetAllClientOrders", HttpMethod.Post, clientId);
        }
    }
}
