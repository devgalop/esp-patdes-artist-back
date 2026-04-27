using CulturalEventsManagement.Infrastructure.Database.CulturalEventManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CulturalEventsManagement.Infrastructure.Database.Shared;

public static class DatabaseExtensions
{
    public static WebApplicationBuilder AddDatabaseDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDatabaseContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        //Agrega mappers
        builder.AddCulturalEventMapper();

        //Agrega repositorios
        builder.AddCulturalEventRepository();
        
        return builder;
    }

    public static WebApplication UseDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
        return app;
    }

    public static WebApplication EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        dbContext.Database.EnsureCreated();
        return app;
    }
}
