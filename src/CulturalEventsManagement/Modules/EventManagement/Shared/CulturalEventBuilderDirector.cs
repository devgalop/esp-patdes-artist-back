using CulturalEventsManagement.Modules.EventManagement.CreateEvent;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.EventManagement.Shared;

public class CulturalEventBuilderDirector(ICulturalEventBuilder builder)
{
    /// <summary>
    /// Construye un evento cultural gratuito a partir de una solicitud de creación de evento.
    /// </summary>
    /// <param name="request">La solicitud de creación de evento.</param>
    /// <returns>El evento cultural construido.</returns>
    public CulturalEvent BuildFreeEvent(CreateEventRequest request)
    {
        return builder
            .WithName(request.Name)
            .WithDescription(request.Description)
            .WithScheduledDate(request.ScheduledAt)
            .WithType(Enum.Parse<EventType>(request.Details.Type, true))
            .WithBillingType(EventBillingType.Free)
            .Build();
    }

    /// <summary>
    /// Construye un evento cultural de pago a partir de una solicitud de creación de evento.
    /// </summary>
    /// <param name="request">La solicitud de creación de evento.</param>
    /// <returns>El evento cultural construido.</returns>
    public CulturalEvent BuildPaidEvent(CreateEventRequest request)
    {
        return builder
            .WithName(request.Name)
            .WithDescription(request.Description)
            .WithScheduledDate(request.ScheduledAt)
            .WithType(Enum.Parse<EventType>(request.Details.Type, true))
            .WithBillingType(EventBillingType.Paid)
            .WithPrice(request.Details.Price)
            .WithLocation(request.Details.Location ?? string.Empty)
            .Build();
    }

    /// <summary>
    /// Construye un evento cultural de donación a partir de una solicitud de creación de evento.
    /// </summary>
    /// <param name="request">La solicitud de creación de evento.</param>
    /// <returns>El evento cultural construido.</returns>
    public CulturalEvent BuildDonationEvent(CreateEventRequest request)
    {
        return builder
            .WithName(request.Name)
            .WithDescription(request.Description)
            .WithScheduledDate(request.ScheduledAt)
            .WithType(Enum.Parse<EventType>(request.Details.Type, true))
            .WithBillingType(EventBillingType.Donation)
            .Build();
    }

    /// <summary>
    /// Construye un evento cultural con tipo de facturación no asignado a partir de una solicitud de creación de evento.
    /// </summary>
    /// <param name="request">La solicitud de creación de evento.</param>
    /// <returns>El evento cultural construido.</returns>
    public CulturalEvent BuildOtherEvent(CreateEventRequest request)
    {
        return builder
            .WithName(request.Name)
            .WithDescription(request.Description)
            .WithScheduledDate(request.ScheduledAt)
            .WithType(Enum.Parse<EventType>(request.Details.Type, true))
            .WithBillingType(EventBillingType.Unassigned)
            .Build();
    }
}
