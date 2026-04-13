using culturalEvents.Modules.EventManagement.Common;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.EventManagement.CreateEvent;

public sealed class CreateEventHandler(
    IEventBuilder eventBuilder
) : ICommandHandler<CreateEventRequest>
{
    public async Task HandleAsync(CreateEventRequest command)
    {
        var culturalEvent = eventBuilder
            .WithName(command.Name)
            .WithDate(command.UtcDate)
            .WithCategory(Enum.Parse<EventCategory>(command.Category))
            .WithValue(Enum.Parse<EventValue>(command.EventType))
            .Build();
        Console.WriteLine($"Event created: {culturalEvent.Name} on {culturalEvent.UtcDate} in category {culturalEvent.Category} with value {culturalEvent.Value}");
        await Task.CompletedTask;
    }
}
