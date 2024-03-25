using Order.Host.Data.Entities;
using Order.Host.Data.Enums;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderBasketService
    {
        Task<int?> AddAsync(int clientId, string name, string lastName, decimal totalPrice, List<AddItemsOrderRequest> basketItems);
        Task<bool> UpdateAsync(int id, OrderStatus orderStatus);
        Task<bool> DeleteAsync(int id);
    }
}
