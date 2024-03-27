using MVC.Models.Requests;
using MVC.ViewModels.Order;

namespace MVC.Services.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrderHistory(OrderHistory order);
        public Task<Order> GetOrderHistory(OrderHistoryRequest clientId);
    }
}
