using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database
{
    public static class BuilderExtensions
    {
        public static ModelBuilder ConfigureUserRelationships(this ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                user.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                user.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(200);

                user.HasKey(u => u.Id);
                user.HasIndex(u => u.Email).IsUnique();

                user.HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .UsingEntity(j => j.ToTable("UserRoles"));  
            });

            builder.Entity<Role>(role =>
            {
                role.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                role.Property(r => r.Description)
                    .HasMaxLength(200);

                role.HasKey(r => r.Id);
                role.HasIndex(r => r.Name)
                    .IsUnique();

                role.HasMany(r => r.Permissions)
                    .WithMany(p => p.Roles)
                    .UsingEntity(j => j.ToTable("RolePermissions"));
            });

            builder.Entity<Permission>(permission =>
            {
                permission.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                permission.Property(p => p.Description)
                    .HasMaxLength(200);

                permission.HasKey(p => p.Id);
                permission.HasIndex(p => p.Name)
                    .IsUnique();
            });

            return builder;
        }

        public static ModelBuilder ConfigureEventRelationships(this ModelBuilder builder)
        {
            builder.Entity<Venue>(entity =>
            {
                entity.Property(v => v.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(v => v.Address)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(v => v.Capacity)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(v => v.RentalPrice)
                    .IsRequired();
                entity.Property(v => v.Status)
                    .IsRequired()
                    .HasConversion<int>();
                

                entity.HasKey(v => v.Id);
                entity.HasIndex(v => v.Name)
                    .IsUnique();

                entity.HasMany(v => v.Seats)
                    .WithOne(s => s.Venue)
                    .HasForeignKey(s => s.VenueId);
                
                entity.HasMany(v => v.Events)
                    .WithOne(e => e.Venue)
                    .HasForeignKey(e => e.VenueId);
            });

            builder.Entity<TicketSeat>(entity =>
            {
                entity.Property(s => s.SeatNumber).HasMaxLength(10);

                entity.HasKey(s => s.Id);

                entity.HasIndex(s => new { s.VenueId, s.SeatNumber })
                    .IsUnique();
            });

            builder.Entity<CulturalEvent>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasConversion<int>();
                
                entity.Property(e => e.Status)                    
                    .IsRequired()
                    .HasConversion<int>();
                
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasConversion<int>();

                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Artist)
                    .WithMany()
                    .HasForeignKey(e => e.ArtistId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Venue)
                    .WithMany(v => v.Events)
                    .HasForeignKey(e => e.VenueId);
            });

            builder.Entity<Ticket>(entity =>
            {
                entity.Property(t => t.UtcReservationDate)
                    .IsRequired();

                entity.HasKey(t => t.Id);

                entity.HasOne(t => t.Event)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(t => t.EventId);

                entity.HasOne(t => t.TicketSeat)
                    .WithMany()
                    .HasForeignKey(t => t.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Offering>(entity =>
            {
                entity.Property(o => o.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(o => o.Description)
                    .HasMaxLength(500);
                entity.Property(o => o.Price)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(o => o.Status)                    
                    .IsRequired()
                    .HasConversion<int>();

                entity.HasKey(o => o.Id);

                entity.HasOne(o => o.Provider)
                    .WithMany()
                    .HasForeignKey(o => o.ProviderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Order>(entity =>
            {
                entity.Property(o => o.UtcCreatedAt)
                    .IsRequired();
                
                entity.HasKey(o => o.Id);

                entity.HasOne(o => o.Artist)
                    .WithMany()
                    .HasForeignKey(o => o.ArtistId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Provider)
                    .WithMany()
                    .HasForeignKey(o => o.ProviderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Event)
                    .WithMany()
                    .HasForeignKey(o => o.EventId);

                entity.HasMany(o => o.Items)
                    .WithOne()
                    .HasForeignKey(i => i.OrderId);
            });

            builder.Entity<OrderItem>(entity =>
            {
                entity.Property(i => i.Quantity)
                    .IsRequired()
                    .HasDefaultValue(0);
                    
                entity.HasKey(i => i.Id);

                entity.HasOne(i => i.Offering)
                    .WithMany()
                    .HasForeignKey(i => i.OfferingId);
            });
    

            return builder;
        }
    }
}