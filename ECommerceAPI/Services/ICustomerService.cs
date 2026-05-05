using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<ServiceResult<CustomerDto>> GetByIdAsync(int id);
        Task<ServiceResult<CustomerDto>> CreateAsync(CreateCustomerDto dto);
        Task<ServiceResult<CustomerDto>> UpdateAsync(int id, UpdateCustomerDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}