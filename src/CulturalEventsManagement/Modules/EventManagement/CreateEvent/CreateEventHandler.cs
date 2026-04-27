using CulturalEventsManagement.Modules.EventManagement.Shared;
using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Modules.EventManagement.CreateEvent;

public class CreateEventHandler(
    ICulturalEventRepository repository,
    ICulturalEventBuilder builder
) : ICommandHandler<CreateEventRequest>
{
    public async Task HandleAsync(CreateEventRequest command)
    {
        var culturalEvent = builder
            .WithName(command.Name)
            .WithDescription(command.Description)
            .WithScheduledDate(command.ScheduledAt)
            .WithType(Enum.Parse<EventType>(command.Details.Type, true))
            .WithBillingType(Enum.Parse<EventBillingType>(command.Details.BillingType, true))
            .Build();

        await repository.SaveAsync(culturalEvent);
    }
}

public static class CreateEventHandlerExtensions
{
    public static WebApplicationBuilder AddCreateEventHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandHandler<CreateEventRequest>, CreateEventHandler>();
        return builder;
    }
}
