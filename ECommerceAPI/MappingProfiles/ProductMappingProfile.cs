using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Mapping dari Product (entity) ke productDTO
            CreateMap<Product, ProductDto>()
                // CategoryName diambil dari relasi Category.Name
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name));

            // Mapping untuk product summaru
            CreateMap<Product, ProductSummaryDto>();

            // Mapping dari create ptoduct ke product
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // Mapping dari UpdateProductDto ke product
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}