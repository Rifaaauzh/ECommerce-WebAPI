using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    // Dipakai saat user membuat order detail di dalam CreateOrderDto
    public class CreateOrderDetailDto
    {
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    // Dipakai untuk response / hasil read order detail
    public class OrderDetailDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
    }
}