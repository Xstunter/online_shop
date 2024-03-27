using AutoMapper;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;

namespace Order.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderHistory, OrderHistoryDto>(); // Настроить маппинг OrderHistory на OrderHistoryDto
        CreateMap<BasketItem, BasketItemDto>();
    }
}