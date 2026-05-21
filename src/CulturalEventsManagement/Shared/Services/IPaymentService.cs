using CulturalEventsManagement.Modules.OrderManagement.PayOrder;
using CulturalEventsManagement.Modules.OrderManagement.Shared;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Shared.Services;

public interface IPaymentService
{
    Task<OrderStatus> ProcessPaymentAsync(PaymentRequest request);
}

public sealed class PaymentService(
    IOrderRepository orderRepository,
    IPaymentGatewayFactory paymentGatewayFactory
) : IPaymentService
{
    public async Task<OrderStatus> ProcessPaymentAsync(PaymentRequest request)
    {
        var order = await orderRepository.GetByIdAsync(Guid.Parse(request.OrderId)) 
                        ?? throw new InvalidOperationException($"Order with ID {request.OrderId} not found.");
        var paymentGateway = paymentGatewayFactory.Create(request.PaymentMethod);
        PaymentOrderContext paymentContext = new(order, paymentGateway, request.PaymentMethod);
        paymentContext.SetState(new PendingPaymentState());
        await paymentContext.DoNext();
        return order.Status;
    }
}

public static class PaymentServiceExtensions
{
    public static WebApplicationBuilder AddPaymentService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        return builder;
    }
}