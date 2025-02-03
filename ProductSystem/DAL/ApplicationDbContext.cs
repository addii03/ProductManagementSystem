using Microsoft.EntityFrameworkCore;
using ProductSystem.Models;

namespace ProductSystem.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) 
            .WithMany(c => c.Products) 
            .HasForeignKey(p => p.CategoryId) 
            .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Category>()
            .HasIndex(c => c.CategoryName)
            .IsUnique();

            modelBuilder.Entity<Product>()
           .HasIndex(p => p.ProductName)
           .IsUnique();
            base.OnModelCreating(modelBuilder);
        }

    }
}
