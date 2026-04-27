using CulturalEventsManagement.Modules.EventManagement.CreateEvent;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.EventManagement.Shared;

public interface ICulturalEventFactory
{
    /// <summary>
    /// Crea un nuevo evento cultural.
    /// </summary>
    /// <param name="type">El tipo de facturación del evento cultural.</param>
    /// <param name="request">La solicitud de creación del evento cultural.</param>
    /// <returns>El evento cultural creado.</returns>
    CulturalEvent Create(EventBillingType type,CreateEventRequest request);
}

public class CulturalEventFactory(
    CulturalEventBuilderDirector builder
) : ICulturalEventFactory
{
    public CulturalEvent Create(EventBillingType type, CreateEventRequest request)
    {
        return type switch
        {
            EventBillingType.Free => builder.BuildFreeEvent(request),
            EventBillingType.Paid => builder.BuildPaidEvent(request),
            EventBillingType.Donation => builder.BuildDonationEvent(request),
            EventBillingType.Unassigned => builder.BuildOtherEvent(request),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
