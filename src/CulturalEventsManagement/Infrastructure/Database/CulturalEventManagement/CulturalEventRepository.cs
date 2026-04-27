using CulturalEventsManagement.Infrastructure.Database.Shared;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CulturalEventsManagement.Infrastructure.Database.CulturalEventManagement;

public class CulturalEventRepository(
    AppDatabaseContext dbContext,
    CulturalEventMapper mapper
) : ICulturalEventRepository
{
    public async Task<IEnumerable<CulturalEvent>> GetAll()
    {
        return await dbContext.CulturalEvents
                                .Select(e => mapper.ToDomain(e))
                                .ToListAsync();
    }

    public async Task<CulturalEvent?> GetByIdAsync(Guid id)
    {
        return await dbContext.CulturalEvents
                                .Where(e => e.Id == id.ToString())
                                .Select(e => mapper.ToDomain(e))
                                .FirstOrDefaultAsync();
    }

    public async Task SaveAsync(CulturalEvent culturalEvent)
    {
        var entity = mapper.ToEntity(culturalEvent);
        var existingEntity = await dbContext.CulturalEvents
                                            .FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (existingEntity == null)
        {
            await dbContext.CulturalEvents.AddAsync(entity);
        }
        else
        {
            dbContext.Update(entity);
        }

        await dbContext.SaveChangesAsync();
    }
}


public static class CulturalEventRepositoryExtensions
{
    public static WebApplicationBuilder AddCulturalEventRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICulturalEventRepository, CulturalEventRepository>();
        return builder;
    }
}