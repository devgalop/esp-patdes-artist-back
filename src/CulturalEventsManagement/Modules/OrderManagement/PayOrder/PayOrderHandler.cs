using CulturalEventsManagement.Modules.OrderManagement.Shared;
using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Services;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class PayOrderHandler(
    IPaymentService paymentService
) : IQueryHandler<PayOrderRequest, PayOrderResponse>
{
    public async Task<PayOrderResponse> HandleAsync(PayOrderRequest query)
    {
        if(!Enum.TryParse<PaymentMethod>(query.PaymentMethod, out var paymentMethod))
        {
            return new PayOrderResponse(false, "Invalid payment method.", "Failed");
        }
        var result = await paymentService.ProcessPaymentAsync(new PaymentRequest(
            query.OrderId,
            paymentMethod
        ));
        
        return result switch
        {
            OrderStatus.Confirmed => new PayOrderResponse(true, "Payment successful.", "Confirmed"),
            OrderStatus.Cancelled => new PayOrderResponse(false, "Payment failed.", "Cancelled"),
            _ => new PayOrderResponse(false, "Payment processing error.", "Failed")
        };
    }
}

public static class PayOrderExtensions
{
    public static WebApplicationBuilder AddPayOrderHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<PayOrderRequest, PayOrderResponse>, PayOrderHandler>();
        return builder;
    }
}


