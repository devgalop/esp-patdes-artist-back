using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public record ReadEventsResponse(
    IEnumerable<CulturalEventSummary> Events
);

public record CulturalEventSummary(
    Guid Id,
    string Name,
    string Description,
    DateTime ScheduledAt,
    string Type,
    string Status,
    string BillingType
);


