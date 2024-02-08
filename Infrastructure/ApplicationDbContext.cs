using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.ManufacturerEmail, p.ProduceDate })
                .IsUnique();

            modelBuilder.Entity<Product>()
               .HasIndex(p => new { p.ProduceDate, p.ManufacturerEmail })
               .IsUnique();
        }
    }
}