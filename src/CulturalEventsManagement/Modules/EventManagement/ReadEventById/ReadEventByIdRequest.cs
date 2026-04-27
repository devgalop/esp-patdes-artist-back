using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEventById;

public record ReadEventByIdRequest(string EventId): IQuery;
