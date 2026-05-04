using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        // DbSet = representasi table di database nya
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer table config
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id); // primary key

                entity.Property(c => c.Name)
                    .IsRequired() // wajib diisi
                    .HasMaxLength(50); // max 50 karakter

                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(c => c.Email)
                    .IsUnique(); // email tidak boleh duplicate
            });

            // Category table config
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Product table config
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)"); // format decimal di DB

                entity.Property(p => p.Stock)
                    .IsRequired();

                // One Category has many Products
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // category tidak bisa dihapus kalau masih punya product
            });

            // Order table config
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.OrderDate)
                    .IsRequired();

                entity.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                // One Customer has many Orders
                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade); // kalau customer dihapus, order ikut terhapus
            });

            // OrderDetail table config
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => od.Id);

                entity.Property(od => od.Quantity)
                    .IsRequired();

                entity.Property(od => od.UnitPrice)
                    .HasColumnType("decimal(18,2)");

                // One Order has many OrderDetails
                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade); // kalau order dihapus, detail ikut terhapus

                // One Product can appear in many OrderDetails
                entity.HasOne(od => od.Product)
                    .WithMany()
                    .HasForeignKey(od => od.ProductId)
                    .OnDelete(DeleteBehavior.Restrict); // product tidak ikut kehapus saat order detail dihapus
            });
        }
    }
}