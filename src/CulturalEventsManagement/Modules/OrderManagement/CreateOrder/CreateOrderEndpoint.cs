using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.OrderManagement.CreateOrder;

public class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async(
            CreateOrderRequest request,
            IMediator mediator,
            IValidator<CreateOrderRequest> validator
        )=>
        {
            validator.ValidateAndThrow(request);
            var response = await mediator.SendAsync<CreateOrderRequest, CreateOrderResponse>(request);
            return Results.Ok(response);
        });
    }
}
