namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed record PayOrderResponse
(
    bool IsSuccess,
    string Message,
    string FinalStatus
);

