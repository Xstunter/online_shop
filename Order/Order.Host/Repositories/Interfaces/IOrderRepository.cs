using Order.Host.Models.Dtos;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<OrderHistoryDto>> GetAllOrders(int clientId);
        public Task<OrderHistoryDto> GetOrder(int id);
    }
}
