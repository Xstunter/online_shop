using Order.Host.Data;
using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<PaginatedOrder<OrderHistory>> GetAllClientOrdersAsync(int clientId);
        public Task<PaginatedOrder<OrderHistory>> GetClientOrderAsync(int id);
    }
}
