using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult<IEnumerable<OrderDto>>> GetAllAsync();
        Task<ServiceResult<OrderDto>> GetByIdAsync(int id);
        Task<ServiceResult<OrderDto>> CreateAsync(CreateOrderDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}