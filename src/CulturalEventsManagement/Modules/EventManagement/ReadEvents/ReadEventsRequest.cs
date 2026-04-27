using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEvents;

public record ReadEventsRequest(int PageNumber, int PageSize): IQuery;
