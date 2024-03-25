using AutoMapper;
using Order.Host.Data;
using Order.Host.Models;
using Order.Host.Models.Dtos;
using Order.Host.Models.Response;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;
using System.Drawing.Printing;
using static ServiceStack.Diagnostics.Events;

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
        public async Task<PaginatedOrdersResponse<OrderHistoryDto>?> GetAllClientOrdersAsync(int clientId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.GetAllClientOrdersAsync(clientId);
                if (result == null)
                {
                    return null;
                }

                return new PaginatedOrdersResponse<OrderHistoryDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<OrderHistoryDto>(s)).ToList()
                };
            });
        }

        public async Task<PaginatedOrderResponse<OrderHistoryDto>?> GetClientOrderAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.GetClientOrderAsync(id);
                if (result == null)
                {
                    return null;
                }
                return new PaginatedOrderResponse<OrderHistoryDto>()
                {
                    Data = result.Data.Select(s => _mapper.Map<OrderHistoryDto>(s)).ToList()
                };
            });
        }
    }
}
