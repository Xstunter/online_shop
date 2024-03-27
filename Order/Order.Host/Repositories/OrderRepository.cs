using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;
using static ServiceStack.Diagnostics.Events;

namespace Order.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<OrderRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }
        public async Task<PaginatedOrder<OrderHistory>> GetAllClientOrdersAsync(string clientId)
        {
            try
            {
                var totalItems = await _dbContext.OrderHistories
                 .Where(i => i.ClientId == clientId)
                 .LongCountAsync();

                if (totalItems == 0)
                {
                    throw new ArgumentNullException($"Not found order with clientId:{clientId}!");
                }

                var result = await _dbContext.OrderHistories
               .Where(i => i.ClientId == clientId)
               .OrderBy(i => i.Id)
               .Include(i => i.BasketItems)
               .ToListAsync();

                return new PaginatedOrder<OrderHistory>() { TotalCount = totalItems, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new PaginatedOrder<OrderHistory>();
            }
        }

        public async Task<PaginatedOrder<OrderHistory>> GetClientOrderAsync(int id)
        {
            try
            {
                var result = await _dbContext.OrderHistories
                .Include(i => i.BasketItems)
                .Where(i => i.Id == id)
                .ToListAsync();
                
                if(result.Count == 0)
                {
                    throw new ArgumentNullException($"Not found order with id:{id}!");
                }

                return new PaginatedOrder<OrderHistory>() { Data = result };

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new PaginatedOrder<OrderHistory>();
            }
        }
    }
}
