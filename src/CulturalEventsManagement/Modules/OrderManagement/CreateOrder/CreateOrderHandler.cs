using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Services;

namespace CulturalEventsManagement.Modules.OrderManagement.CreateOrder;

public class CreateOrderHandler(
    IOrderService orderService
) : IQueryHandler<CreateOrderRequest, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> HandleAsync(CreateOrderRequest query)
    {
        OrderRequest orderRequest = new(
            UserId: query.UserId,
            EventId: query.EventId,
            ProviderId: query.ProviderId,
            Details: query.Details.Select(d => new OrderDetail(d.ProductId, d.Quantity)).ToList(),
            CuponCode: query.CuponCode,
            Observations: query.Observations
        );
        var orderId = await orderService.CreateAsync(orderRequest);
        if(string.IsNullOrEmpty(orderId))
            return new CreateOrderResponse(false, "Failed to create order.", orderId);
        return new CreateOrderResponse(true, "Order created successfully.", orderId);
    }
}

public static class CreateOrderHandlerExtensions
{
    public static WebApplicationBuilder AddCreateOrderHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<CreateOrderRequest, CreateOrderResponse>, CreateOrderHandler>();
        return builder;
    }
}
