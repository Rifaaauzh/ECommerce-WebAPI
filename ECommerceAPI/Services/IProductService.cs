using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<IEnumerable<ProductDto>>> GetAllAsync();
        Task<ServiceResult<ProductDto>> GetByIdAsync(int id);
        Task<ServiceResult<ProductDto>> CreateAsync(CreateProductDto dto);
        Task<ServiceResult<ProductDto>> UpdateAsync(int id, UpdateProductDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}