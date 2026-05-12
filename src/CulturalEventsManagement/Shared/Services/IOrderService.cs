using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;
using CulturalEventsManagement.Modules.NotificationManagement;
using CulturalEventsManagement.Modules.NotificationManagement.SendNotification;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Exceptions;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Shared.Services;

public sealed record OrderRequest(
    string UserId,
    string EventId,
    string ProviderId,
    List<OrderDetail> Details,
    string? CuponCode,
    string? Observations
);

public sealed record OrderDetail(
    string ProductId,
    int Quantity
);

public interface IOrderService
{
    Task<string> CreateAsync(OrderRequest request);
}

public sealed class OrderMediatorService(
    ICatalogService catalogService,
    ICulturalEventRepository eventRepository,
    IOrderRepository orderRepository,
    NotificationOrchestrator notificationService
) : IOrderService
{
    public async Task<string> CreateAsync(OrderRequest request)
    {
        //Valida que exista el evento cultural
        _ = await eventRepository.GetByIdAsync(Guid.Parse(request.EventId))
                                        ?? throw new CulturalEventNotFoundException(request.EventId);
        // Valida que el proveedor exista y tenga catalogo disponible
        var catalog = await catalogService.GetCatalogByProviderAsync(request.ProviderId) 
                                        ?? throw new InvalidProviderException(request.ProviderId);

        if(catalog.Items == null || !catalog.Items.Any())
            throw new InvalidProviderException(request.ProviderId);
        
        List<OrderItemDetail> orderDetails = []; 
        // Valida que los productos solicitados existan en el catalogo del proveedor
        foreach (var product in request.Details)
        {
            if (!catalog.Items.Any(p => p.Id == product.ProductId))
                throw new ProductNotFoundException(product.ProductId, request.ProviderId);
            orderDetails.Add(new OrderItemDetail(product.ProductId, product.Quantity));
        }
        
        var order = new Order(
            request.UserId,
            Guid.Parse(request.EventId),
            request.ProviderId,
            request.CuponCode ?? string.Empty,
            request.Observations ?? string.Empty,
            orderDetails
        );
        await orderRepository.AddAsync(order);

        //Enviar notificación de nuevo pedido
        await notificationService.NotificateAsync(
            new UserNotificationConfig(
                UserId: request.UserId,
                SmsEnabled: true,
                WhatsappEnabled: false,
                TelegramEnabled: false
            ),
            new BaseNotificationRequest(
                Sender: "Order Service",
                Recipient: request.UserId,
                Message: $"Your order with id {order.Id} has been created successfully."
            )
        );

        await notificationService.NotificateAsync(
            new UserNotificationConfig(
                UserId: request.ProviderId,
                SmsEnabled: true,
                WhatsappEnabled: true,
                TelegramEnabled: true
            ),
            new BaseNotificationRequest(
                Sender: "Order Service",
                Recipient: request.ProviderId,
                Message: $"A new order with id {order.Id} has been created for your catalog."
            )
        );

        return order.Id.ToString();
    }
}

public static class OrderMediatorServiceExtensions
{
    public static WebApplicationBuilder AddOrderMediatorService(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderService, OrderMediatorService>();
        return builder;
    }
}