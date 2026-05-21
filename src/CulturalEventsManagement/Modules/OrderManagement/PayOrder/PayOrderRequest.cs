using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed record PayOrderRequest
(
    string OrderId,
    string PaymentMethod
): IQuery;

