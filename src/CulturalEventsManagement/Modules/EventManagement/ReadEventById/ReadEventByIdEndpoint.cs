using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEventById;

public class ReadEventByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/events/{id}", async(
            string id,
            IMediator mediator
        ) =>
        {
            if(string.IsNullOrEmpty(id))
            {
                return Results.BadRequest(new ReadEventByIdResponse(
                    IsSuccess: false,
                    Message: "El ID del evento no puede ser nulo o vacío",
                    Event: null
                ));
            }
            var response = await mediator.SendAsync<ReadEventByIdRequest, ReadEventByIdResponse>(
                new ReadEventByIdRequest(id)
            );
            if (!response.IsSuccess)
            {
                return Results.NotFound(response);
            }
            return Results.Ok(response);
        })
        .WithName("GetEventById")
        .WithTags("Event Management")
        .WithDescription("Recupera un evento cultural por su ID")
        .Produces<ReadEventByIdResponse>(StatusCodes.Status200OK)
        .Produces<ReadEventByIdResponse>(StatusCodes.Status400BadRequest)
        .Produces<ReadEventByIdResponse>(StatusCodes.Status404NotFound);
    }
}
