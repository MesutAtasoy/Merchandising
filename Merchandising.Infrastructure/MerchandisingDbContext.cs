using System.Reflection;
using Merchandising.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merchandising.Infrastructure;

public class MerchandisingDbContext : DbContext
{
    public MerchandisingDbContext(DbContextOptions<MerchandisingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product?> Products { get; set; }
    public DbSet<Category?> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MerchandisingDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Product>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>()
            {
                Category.Create(Guid.Parse("c365d08f-7cd4-4b6e-bae3-722bfd51e931"), "Test", 100)
            });
        
        base.OnModelCreating(modelBuilder);
    }
}