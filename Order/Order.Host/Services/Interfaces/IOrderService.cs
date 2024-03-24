using Order.Host.Models.Dtos;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<OrderHistoryDto>> GetAllOrders(int clientId);
        public Task<OrderHistoryDto> GetOrder(int id);
    }
}
