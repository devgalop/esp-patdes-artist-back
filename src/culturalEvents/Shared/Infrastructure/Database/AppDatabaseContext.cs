using culturalEvents.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database
{
    public sealed class AppDatabaseContext(
        DbContextOptions<AppDatabaseContext> options
    ) : DbContext(options)
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<Permission> Permissions {get; set;}
        public DbSet<CulturalEvent> Events {get; set;}
        public DbSet<Venue> Venues {get; set;}
        public DbSet<TicketSeat> Seats {get; set;}
        public DbSet<Ticket> Tickets {get; set;}
        public DbSet<Offering> Offerings {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<OrderItem> OrderItems {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureUserRelationships()
                        .ConfigureEventRelationships();
        }
    }
}