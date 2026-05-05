using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return ServiceResult<IEnumerable<CategoryDto>>.Success(dtos);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return ServiceResult<CategoryDto>.Failure("Category not found");

            var dto = _mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(dto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetWithProductsAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(id);

            if (category == null)
                return ServiceResult<CategoryWithProductsDto>.Failure("Category not found");

            var dto = _mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(dto);
        }

        public async Task<ServiceResult<CategoryDto>> CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return ServiceResult<CategoryDto>.Failure("Category name is required");

            var category = _mapper.Map<Category>(dto);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            var resultDto = _mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(resultDto, "Category created successfully");
        }

        public async Task<ServiceResult<CategoryDto>> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return ServiceResult<CategoryDto>.Failure("Category not found");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return ServiceResult<CategoryDto>.Failure("Category name is required");

            _mapper.Map(dto, category);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            var resultDto = _mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(resultDto, "Category updated successfully");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return ServiceResult<bool>.Failure("Category not found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return ServiceResult<bool>.Success(true, "Category deleted successfully");
        }
    }
}