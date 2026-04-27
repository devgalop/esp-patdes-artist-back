using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.EventManagement.RescheduledEvent;

public class RescheduledEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/events/reschedule", async(
            RescheduledEventRequest request,
            IMediator mediator,
            IValidator<RescheduledEventRequest> validator
        )=>
        {
            await validator.ValidateAndThrowAsync(request);
            var response = await mediator.SendAsync<RescheduledEventRequest, RescheduledEventResponse>(request);
            if(!response.IsSuccess)
            {
                return Results.BadRequest(response);
            }
            return Results.Ok(response);
        })
        .WithName("RescheduleEvent")
        .WithTags("EventManagement")
        .Produces<RescheduledEventResponse>(StatusCodes.Status200OK)
        .Produces<RescheduledEventResponse>(StatusCodes.Status400BadRequest);
    }
}
