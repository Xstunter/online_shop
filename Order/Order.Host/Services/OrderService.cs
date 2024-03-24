using AutoMapper;
using Order.Host.Data;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderRepository orderRepository,
            IMapper mapper)
        : base(dbContextWrapper, logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderHistoryDto>> GetAllOrders(int clientId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return new List<OrderHistoryDto>();
            });
        }

        public Task<OrderHistoryDto> GetOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
