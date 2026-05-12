using CulturalEventsManagement.Infrastructure.Database.Shared.Models;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Infrastructure.Database.OrderManagement;

public sealed class OrderMapper
{
    public OrderEntity ToEntity(Order order)
    {
        return new OrderEntity
        {
            Id = order.Id.ToString(),
            UserId = order.UserId.ToString(),
            EventId = order.EventId.ToString(),
            ProviderId = order.ProviderId.ToString(),
            CuponCode = order.CuponCode,
            Observations = order.Observations
        };
    }

    public OrderDetailEntity ToDetailEntity(OrderItemDetail detail, string orderId)
    {
        return new OrderDetailEntity
        {
            Id = Guid.NewGuid().ToString(),
            OrderId = orderId,
            CatalogItemId = detail.ProductId,
            Quantity = detail.Quantity
        };
    }

    public Order ToDomain(OrderEntity entity)
    {
        var details = entity.OrderDetails.Select(d => new OrderItemDetail(d.CatalogItemId, d.Quantity)).ToList();

        return new Order(
            entity.UserId,
            Guid.Parse(entity.EventId),
            entity.ProviderId,
            entity.CuponCode,
            entity.Observations,
            details
        );
    }

    public OrderItemDetail ToDomainDetail(OrderDetailEntity entity)
    {
        return new OrderItemDetail(entity.CatalogItemId, entity.Quantity);
    }
}

public static class OrderMapperExtensions
{
    public static WebApplicationBuilder AddOrderMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<OrderMapper>();
        return builder;
    }
}
