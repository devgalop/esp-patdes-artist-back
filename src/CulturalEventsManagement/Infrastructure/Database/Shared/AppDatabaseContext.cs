
using CulturalEventsManagement.Infrastructure.Database.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CulturalEventsManagement.Infrastructure.Database.Shared;

public class AppDatabaseContext(
    DbContextOptions<AppDatabaseContext> options
) : DbContext(options)
{
    public DbSet<CulturalEventEntity> CulturalEvents { get; set; }

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
    }
}
