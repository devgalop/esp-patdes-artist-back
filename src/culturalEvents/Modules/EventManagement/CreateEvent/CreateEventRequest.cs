using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Modules.EventManagement.CreateEvent;

public sealed record CreateEventRequest(
    string Name,
    DateTime UtcDate,
    string Category,
    string EventType
): ICommand;
