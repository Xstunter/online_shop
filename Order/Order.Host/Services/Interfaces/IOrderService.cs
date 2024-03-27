using Order.Host.Models;
using Order.Host.Models.Dtos;
using Order.Host.Models.Response;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<PaginatedOrdersResponse<OrderHistoryDto>?> GetAllClientOrdersAsync(string clientId);
        public Task<PaginatedOrderResponse<OrderHistoryDto>?> GetClientOrderAsync(int id);
    }
}
