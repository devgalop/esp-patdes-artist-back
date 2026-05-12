namespace CulturalEventsManagement.Modules.OrderManagement.CreateOrder;

public sealed record CreateOrderResponse(
    bool IsSuccess,
    string Message,
    string? OrderId
);
