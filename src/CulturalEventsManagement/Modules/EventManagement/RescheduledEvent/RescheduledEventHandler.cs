using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Modules.EventManagement.RescheduledEvent;

public class RescheduledEventHandler (
    ICulturalEventRepository repository
): IQueryHandler<RescheduledEventRequest, RescheduledEventResponse>
{
    public async Task<RescheduledEventResponse> HandleAsync(RescheduledEventRequest query)
    {
        var eventFound = await repository.GetByIdAsync(Guid.Parse(query.EventId));
        if(eventFound is null)
        {
            return new RescheduledEventResponse(false, "El evento no existe o ha sido eliminado.");
        }
        eventFound.AssignDate(query.NewDate);
        await repository.SaveAsync(eventFound);
        return new RescheduledEventResponse(true, "El evento ha sido reprogramado exitosamente.");
    }
}

public static class RescheduledEventHandlerExtensions
{
    public static WebApplicationBuilder AddRescheduledEventHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<RescheduledEventRequest, RescheduledEventResponse>, RescheduledEventHandler>();
        return builder;
    }
}
