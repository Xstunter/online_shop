using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Data.Enums;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderBasketRepository : IOrderBasketRepository
    {
        private readonly ILogger<OrderBasketRepository> _logger;
        private readonly ApplicationDbContext _dbContext;
        public OrderBasketRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderBasketRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContextWrapper.DbContext;
        }
        public async Task<int?> AddAsync(string clientId, string name, string lastName, decimal totalPrice, List<AddItemsOrderRequest> requestBasketItems)
        {
            var basketItems = requestBasketItems.Select(x => new BasketItem { ItemId = x.ItemId, Amount = x.Amount, Name = x.Name, Price = x.Price }).ToList();

            var item = await _dbContext.AddAsync(new OrderHistory
            {
                ClientId = clientId,
                Name = name,
                LastName = lastName,
                TotalPrice = totalPrice,
                BasketItems = basketItems, 
                OrderStatus = OrderStatus.Pending
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = _dbContext.OrderHistories.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found order with id:{id}!");
                }

                _dbContext.OrderHistories.Remove(item);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, OrderStatus orderStatus)
        {
            try
            {
                var item = _dbContext.OrderHistories.FirstOrDefault(t => t.Id == id);

                if (item == null)
                {
                    throw new ArgumentNullException($"Not found order with id:{id}!");
                }

                item.OrderStatus = orderStatus;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
    }
}
