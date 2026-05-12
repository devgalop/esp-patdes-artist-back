
using CulturalEventsManagement.Infrastructure.Database.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CulturalEventsManagement.Infrastructure.Database.Shared;

public class AppDatabaseContext(
    DbContextOptions<AppDatabaseContext> options
) : DbContext(options)
{
    public DbSet<CulturalEventEntity> CulturalEvents { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CulturalEventEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.EventId).IsRequired();
            entity.Property(e => e.ProviderId).IsRequired();
            entity.Property(e => e.CuponCode);
            entity.Property(e => e.Observations);
        });

        modelBuilder.Entity<OrderDetailEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderId).IsRequired();
            entity.Property(e => e.CatalogItemId).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();

            entity.HasOne<OrderEntity>()
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
