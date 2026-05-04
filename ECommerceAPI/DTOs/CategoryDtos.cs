using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    // CREATE
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }
    // UPDATE
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }

    // Dto nya
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<ProductSummaryDto> Products { get; set; } = new();
    }
}