using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public class ReadEventsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/events", async(
            int pageNumber,
            int pageSize,
            IMediator mediator
        ) =>
        {
            var response = await mediator.SendAsync<ReadEventsRequest, ReadEventsResponse>(
                new ReadEventsRequest(pageNumber, pageSize)
            );
            return Results.Ok(response);
        })
        .WithName("ReadEvents")
        .WithTags("Event Management")
        .WithDescription("Endpoint para obtener una lista paginada de eventos culturales.")
        .Produces<ReadEventsResponse>(StatusCodes.Status200OK);
    }
}
