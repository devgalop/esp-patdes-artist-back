using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class PayOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/pay-order", async (PayOrderRequest request, IMediator mediator) =>
        {
            var response = await mediator.SendAsync<PayOrderRequest, PayOrderResponse>(request);
            return Results.Ok(response);
        })
        .WithName("PayOrder")
        .WithTags("Order Management");
    }
}
