using CulturalEventsManagement.Modules.OrderManagement.CreateOrder;

namespace CulturalEventsManagement.Modules.OrderManagement.Shared;

public static class OrderExtensions
{
    public static WebApplicationBuilder AddOrderExtensions(this WebApplicationBuilder builder)
    {
        builder.AddCreateOrderHandler();
        return builder;
    }
}
