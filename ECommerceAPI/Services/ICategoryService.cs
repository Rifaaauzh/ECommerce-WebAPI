using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResult<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryWithProductsDto>> GetWithProductsAsync(int id);
        Task<ServiceResult<CategoryDto>> CreateAsync(CreateCategoryDto dto);
        Task<ServiceResult<CategoryDto>> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}