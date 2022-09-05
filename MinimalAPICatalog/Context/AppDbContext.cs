using Microsoft.EntityFrameworkCore;
using MinimalAPICatalog.Models;

namespace MinimalAPICatalog.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definições para Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(150)
                .IsRequired();

            // Definições para Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(c => c.Description)
                .HasMaxLength(150);
            modelBuilder.Entity<Product>()
                .Property(c => c.Image)
                .HasMaxLength(100);
            modelBuilder.Entity<Product>()
                .Property(c => c.Price)
                .HasPrecision(14, 2);

            // Relacionamentos
            modelBuilder.Entity<Product>()
                .HasOne<Category>(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId);            
        }
    }
}
