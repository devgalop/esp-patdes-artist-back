namespace CulturalEventsManagement.Modules.EventManagement.ReadEventById;

public record ReadEventByIdResponse(
    bool IsSuccess,
    string Message,
    CulturalEventDetail? Event
);

public record CulturalEventDetail(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime ScheduledAt,
    string Type,
    string Status,
    string BillingType,
    decimal Price,
    string Location
);
