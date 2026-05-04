namespace ECommerceAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Relasi ke Produk
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}