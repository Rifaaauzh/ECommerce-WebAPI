using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;

namespace ECommerceAPI.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // dari CreateCustomerDto ke Entity Customer
            CreateMap<CreateCustomerDto, Customer>();
            
            // dari Entity Customer ke CustomerDto (untuk response)
            CreateMap<Customer, CustomerDto>();
            
            // yaa dari UpdateCustomerDto ke Entity Customer
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }
}