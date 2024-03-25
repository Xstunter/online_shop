using Order.Host.Data;
using Order.Host.Models.Dtos;
using Order.Host.Services.Interfaces;
using static ServiceStack.Diagnostics.Events;
using System.Xml.Linq;
using Order.Host.Repositories.Interfaces;
using Order.Host.Repositories;
using Order.Host.Data.Entities;
using Order.Host.Models.Requests;
using Order.Host.Data.Enums;

namespace Order.Host.Services
{
    public class OrderBasketService : BaseDataService<ApplicationDbContext>, IOrderBasketService
    {
        private readonly IOrderBasketRepository _orderBasketRepository;

        public OrderBasketService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderBasketRepository orderBasketRepository)
                : base(dbContextWrapper, logger)
        {
            _orderBasketRepository = orderBasketRepository;
        }
        public Task<int?> AddAsync(int clientId, string name, string lastName, decimal totalPrice, List<AddItemsOrderRequest> basketItems)
        {
            return ExecuteSafeAsync(() => _orderBasketRepository.AddAsync(clientId, name, lastName, totalPrice, basketItems));

        }
        public Task<bool> DeleteAsync(int id)
        {
            return ExecuteSafeAsync(() => _orderBasketRepository.DeleteAsync(id));
        }

        public Task<bool> UpdateAsync(int id, OrderStatus orderStatus)
        {
            return ExecuteSafeAsync(() => _orderBasketRepository.UpdateAsync(id, orderStatus));
        }
    }
}
