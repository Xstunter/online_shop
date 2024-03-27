using Order.Host.Data.Entities;
using Order.Host.Data.Enums;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderBasketRepository
    {
        Task<int?> AddAsync(string clientId, string name, string lastName, decimal totalPrice, List<AddItemsOrderRequest> basketItems);
        Task<bool> UpdateAsync(int id, OrderStatus orderStatus);
        Task<bool> DeleteAsync(int id);
    }
}
