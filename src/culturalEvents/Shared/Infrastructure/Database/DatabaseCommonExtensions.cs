using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Domain;
using culturalEvents.Shared.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database
{
    public static class DatabaseCommonExtensions
    {
        /// <summary>
        /// Adds the AppDatabaseContext to the service collection and configures it to use PostgreSQL with the connection string from the configuration. This method also applies snake case naming convention for the database tables and columns.
        /// </summary>
        /// <param name="builder">The web application builder used to configure services.</param>
        /// <returns>The web application builder with the database context added.</returns>
        public static WebApplicationBuilder AddDatabaseContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDatabaseContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Postgresql");
                options.UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention(); 
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }

        /// <summary>
        /// Verifies the database connection and logs the result.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>A task that represents the asynchronous operation of verifying the database connection.</returns>
        public static async Task EnsureDatabaseCanConnectAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var canConnect = await db.Database.CanConnectAsync();
            app.Logger.LogInformation("Can connect to database: {CanConnect}", canConnect);
        }

        /// <summary>
        /// Applies any pending migrations to the database.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>A task that represents the asynchronous operation of applying migrations.</returns>
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using AppDatabaseContext appContext =
                scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

            try
            {
                await appContext.Database.MigrateAsync();
                app.Logger.LogInformation("Applied database migrations successfully.");
            }
            catch (Exception e)
            {
                app.Logger.LogError(e, "An error occurred while applying database migrations.");
            }
        }

        /// <summary>
        /// Resets the database by deleting it and applying migrations.
        /// This is intended for use in development or testing environments only.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>A task that represents the asynchronous operation of resetting the database.</returns>
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using AppDatabaseContext appContext =
                scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

            try
            {
                await appContext.Database.EnsureDeletedAsync();
                await appContext.Database.MigrateAsync();
                app.Logger.LogInformation("Database reset successfully.");
            }
            catch (Exception e)
            {
                app.Logger.LogError(e, "An error occurred while resetting the database.");
            }
        }

        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>A task that represents the asynchronous operation of seeding the database.</returns>
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using AppDatabaseContext appContext =
                scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            using var transaction = await appContext.Database.BeginTransactionAsync();
            try
            {
                if (await appContext.Roles.AnyAsync() || await appContext.Permissions.AnyAsync())
                {
                    app.Logger.LogInformation("The database already contains data. Initial seeding will not be performed.");
                    await transaction.RollbackAsync();
                    return;
                }
                
                //Create initial permissions
                Permission createEventPermission = new Permission("CreateEvent", "Permission to create events");
                await appContext.Permissions.AddAsync(createEventPermission);
                Permission createOfferingPermission = new Permission("CreateOffering", "Permission to create offerings");
                await appContext.Permissions.AddAsync(createOfferingPermission);
                Permission createVenuePermission = new Permission("CreateVenue", "Permission to create venues");
                await appContext.Permissions.AddAsync(createVenuePermission);

                //Create initial roles
                Role adminRole = new Role("Admin", "Administrator role with all permissions");
                adminRole.AddPermission(createEventPermission);
                adminRole.AddPermission(createOfferingPermission);
                adminRole.AddPermission(createVenuePermission);
                await appContext.Roles.AddAsync(adminRole);

                Role providerRole = new Role("Provider", "Provider role with permissions to create offerings and venues");
                providerRole.AddPermission(createOfferingPermission);
                providerRole.AddPermission(createVenuePermission);
                await appContext.Roles.AddAsync(providerRole);

                Role artistRole = new Role("Artist", "Artist role with permission to create events");
                artistRole.AddPermission(createEventPermission);
                await appContext.Roles.AddAsync(artistRole);

                await appContext.SaveChangesAsync();
                await transaction.CommitAsync();

                app.Logger.LogInformation("Database seeded successfully with initial roles and permissions.");
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                app.Logger.LogError(e, "An error occurred while seeding the initial data in the database.");
            }
        }
    }
}