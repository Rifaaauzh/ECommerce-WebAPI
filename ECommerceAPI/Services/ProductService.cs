using AutoMapper;
using ECommerceAPI.DTOs;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using ECommerceAPI.Services.Interfaces;

namespace ECommerceAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return ServiceResult<IEnumerable<ProductDto>>.Success(
                productDtos,
                "Products retrieved successfully"
            );
        }

        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetProductWithCategoryAsync(id);

            if (product == null)
            {
                return ServiceResult<ProductDto>.Failure("Product not found");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(
                productDto,
                "Product retrieved successfully"
            );
        }

        public async Task<ServiceResult<ProductDto>> CreateAsync(CreateProductDto dto)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
            {
                return ServiceResult<ProductDto>.Failure("Category not found");
            }

            if (dto.Price <= 0)
            {
                return ServiceResult<ProductDto>.Failure("Price must be greater than 0");
            }

            if (dto.Stock < 0)
            {
                return ServiceResult<ProductDto>.Failure("Stock cannot be negative");
            }

            var product = _mapper.Map<Product>(dto);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            var createdProduct = await _productRepository.GetProductWithCategoryAsync(product.Id);

            var productDto = _mapper.Map<ProductDto>(createdProduct);

            return ServiceResult<ProductDto>.Success(
                productDto,
                "Product created successfully"
            );
        }

        public async Task<ServiceResult<ProductDto>> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResult<ProductDto>.Failure("Product not found");
            }

            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
            {
                return ServiceResult<ProductDto>.Failure("Category not found");
            }

            if (dto.Price <= 0)
            {
                return ServiceResult<ProductDto>.Failure("Price must be greater than 0");
            }

            if (dto.Stock < 0)
            {
                return ServiceResult<ProductDto>.Failure("Stock cannot be negative");
            }

            _mapper.Map(dto, product);

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            var updatedProduct = await _productRepository.GetProductWithCategoryAsync(id);

            var productDto = _mapper.Map<ProductDto>(updatedProduct);

            return ServiceResult<ProductDto>.Success(
                productDto,
                "Product updated successfully"
            );
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ServiceResult<bool>.Failure("Product not found");
            }

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();

            return ServiceResult<bool>.Success(
                true,
                "Product deleted successfully"
            );
        }
    }
}