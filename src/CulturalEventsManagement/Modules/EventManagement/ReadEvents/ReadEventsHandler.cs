using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public class ReadEventsHandler(
    ICulturalEventRepository repository
) : IQueryHandler<ReadEventsRequest, ReadEventsResponse>
{
    public async Task<ReadEventsResponse> HandleAsync(ReadEventsRequest query)
    {
        var events = await repository.GetAll();
        return new ReadEventsResponse(
            events.Select(e => new CulturalEventSummary(
                e.Id,
                e.Name,
                e.Description,
                e.ScheduledAt,
                e.Type.ToString(),
                e.Status.ToString(),
                e.BillingType.ToString()
            ))
        );
    }
}

public static class ReadEventsHandlerExtensions
{
    public static WebApplicationBuilder AddReadEventsHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<ReadEventsRequest, ReadEventsResponse>, ReadEventsHandler>();
        return builder;
    }
}
