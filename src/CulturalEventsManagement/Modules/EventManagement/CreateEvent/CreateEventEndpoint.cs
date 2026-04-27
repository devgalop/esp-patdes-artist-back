using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.EventManagement.CreateEvent;

public class CreateEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/events", async (
            CreateEventRequest request,
            IMediator mediator,
            IValidator<CreateEventRequest> validator) =>
        {
            validator.ValidateAndThrow(request);
            await mediator.SendAsync(request);
            return Results.Json(
                new { Message = "Evento creado exitosamente." }, 
                statusCode: StatusCodes.Status201Created
            );
        })
        .WithName("CreateEvent")
        .WithTags("Event Management")
        .WithDescription("Endpoint para crear un nuevo evento cultural.")
        .Produces(StatusCodes.Status201Created)
        .ProducesValidationProblem();
    }
}
