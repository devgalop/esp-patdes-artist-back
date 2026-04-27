using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public record ReadEventsResponse(
    IEnumerable<CulturalEvent> Events
);


