using CulturalEventsManagement.Infrastructure.Database.Shared.Models;
using CulturalEventsManagement.Modules.EventManagement.Shared;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Infrastructure.Database.CulturalEventManagement;

public class CulturalEventMapper
{
    public CulturalEventEntity ToEntity(CulturalEvent culturalEvent)
    {
        return new CulturalEventEntity
        {
            Id = culturalEvent.Id.ToString(),
            Name = culturalEvent.Name,
            Description = culturalEvent.Description,
            CreatedAt = culturalEvent.CreatedAt,
            ScheduledAt = culturalEvent.ScheduledAt,
            Type = culturalEvent.Type.ToString(),
            Status = culturalEvent.Status.ToString(),
            BillingType = culturalEvent.BillingType.ToString(),
            Price = culturalEvent.Price,
            Location = culturalEvent.Location
        };
    }

    public CulturalEvent ToDomain(CulturalEventEntity entity)
    {
        ICulturalEventBuilder builder = new CulturalEventBuilder();
        return builder.WithId(entity.Id)
                      .WithName(entity.Name)
                      .WithDescription(entity.Description)
                      .WithBillingType(Enum.Parse<EventBillingType>(entity.BillingType))
                      .WithType(Enum.Parse<EventType>(entity.Type))
                      .WithScheduledDate(entity.ScheduledAt)
                      .WithPrice(entity.Price)
                      .WithLocation(entity.Location)
                      .Build();
    }
}

public static class CulturalEventMapperExtensions
{
    /// <summary>
    /// Registra el mapper de eventos culturales en el contenedor de dependencias.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web actualizado.</returns>
    public static WebApplicationBuilder AddCulturalEventMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CulturalEventMapper>();
        return builder;
    }
}
