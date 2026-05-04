using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // me read Order entity -> OrderDto response
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : "Unknown"));

            // nge read OrderDetail entity -> OrderDetailDto response
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : "Unknown"))
                .ForMember(dest => dest.Subtotal,
                    opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        }
    }
}