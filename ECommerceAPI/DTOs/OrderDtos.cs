using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    // CREATE ORDER
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "OrderDetails is required")]
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
    }

    // READ ORDER
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }
}