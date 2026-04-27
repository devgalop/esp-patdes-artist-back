using CulturalEventsManagement.Modules.EventManagement.CreateEvent;
using CulturalEventsManagement.Modules.EventManagement.ReadEventById;
using CulturalEventsManagement.Modules.EventManagement.ReadEvents;
using CulturalEventsManagement.Modules.EventManagement.RescheduledEvent;

namespace CulturalEventsManagement.Modules.EventManagement.Shared;

public static class CulturalEventExtensions
{
    public static WebApplicationBuilder AddCulturalEventModule(this WebApplicationBuilder builder)
    {
        builder.AddCulturalEventBuilder()
                .AddCreateEventHandler()
                .AddReadEventsHandler()
                .AddReadEventByIdHandler()
                .AddRescheduledEventHandler();
                
        return builder;
    }
}
