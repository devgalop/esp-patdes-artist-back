using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.EventManagement.Shared;

public interface ICulturalEventBuilder
{
    /// <summary>
    /// Agrega el ID del evento cultural al builder.
    /// </summary>
    /// <param name="id">El ID del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithId(string id);
    /// <summary>
    /// Agrega el nombre del evento cultural al builder.
    /// </summary>
    /// <param name="name">El nombre del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithName(string name);
    /// <summary>
    /// Agrega la descripción del evento cultural al builder.
    /// </summary>
    /// <param name="description">La descripción del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithDescription(string description);
    /// <summary>
    /// Agrega la fecha programada del evento cultural al builder.
    /// </summary>
    /// <param name="scheduledAt">La fecha programada del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithScheduledDate(DateTime scheduledAt);
    /// <summary>
    /// Agrega el tipo del evento cultural al builder.
    /// </summary>
    /// <param name="type">El tipo del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithType(EventType type);
    /// <summary>
    /// Agrega el tipo de facturación del evento cultural al builder.
    /// </summary>
    /// <param name="billingType">El tipo de facturación del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithBillingType(EventBillingType billingType);

    /// <summary>
    /// Agrega el precio del evento cultural al builder.
    /// </summary>
    /// <param name="price">El precio del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithPrice(decimal price);

    /// <summary>
    /// Agrega la ubicación para sentarse del evento cultural al builder.
    /// </summary>
    /// <param name="location">La ubicación para sentarse del evento cultural.</param>
    /// <returns>El builder actualizado.</returns>
    ICulturalEventBuilder WithLocation(string location);

    /// <summary>
    /// Construye el evento cultural.
    /// </summary>
    /// <returns>El evento cultural construido.</returns>
    CulturalEvent Build();
}

public class CulturalEventBuilder: ICulturalEventBuilder
{
    private readonly CulturalEvent _culturalEvent;

    public CulturalEventBuilder()
    {
        _culturalEvent = new CulturalEvent();
    }
    public ICulturalEventBuilder WithId(string id)
    {
        _culturalEvent.AssignId(Guid.Parse(id));
        return this;
    }

    public ICulturalEventBuilder WithScheduledDate(DateTime scheduledAt)
    {
        _culturalEvent.AssignDate(scheduledAt);
        return this;
    }

    public ICulturalEventBuilder WithType(EventType type)
    {
        _culturalEvent.UpdateType(type);
        return this;
    }

    public ICulturalEventBuilder WithBillingType(EventBillingType billingType)
    {
        _culturalEvent.UpdateBillingType(billingType);
        return this;
    }
    
    public ICulturalEventBuilder WithName(string name)
    {
        _culturalEvent.UpdateName(name);
        return this;
    }

    public ICulturalEventBuilder WithDescription(string description)
    {
        _culturalEvent.UpdateDescription(description);
        return this;
    }

    public CulturalEvent Build()
    {
        return _culturalEvent;
    }

    public ICulturalEventBuilder WithPrice(decimal price)
    {
        _culturalEvent.AssignPrice(price);
        return this;
    }

    public ICulturalEventBuilder WithLocation(string location)
    {
        _culturalEvent.AssignLocation(location);
        return this;
    }
}

public static class CulturalEventBuilderExtensions
{
    /// <summary>
    /// Agrega el builder de eventos culturales al contenedor de servicios.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El constructor de la aplicación web actualizado.</returns>
    public static WebApplicationBuilder AddCulturalEventBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICulturalEventBuilder, CulturalEventBuilder>();
        builder.Services.AddScoped<CulturalEventBuilderDirector>();
        return builder;
    }
}

