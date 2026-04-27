using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public class ReadEventsHandler(
    ICulturalEventRepository repository
) : IQueryHandler<ReadEventsRequest, ReadEventsResponse>
{
    public async Task<ReadEventsResponse> HandleAsync(ReadEventsRequest query)
    {
        return new ReadEventsResponse(
            await repository.GetAll()
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
