using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryWithProductsDto>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}