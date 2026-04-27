using CulturalEventsManagement.Modules.EventManagement.CreateEvent;
using CulturalEventsManagement.Modules.EventManagement.ReadEventById;
using CulturalEventsManagement.Modules.EventManagement.ReadEvents;

namespace CulturalEventsManagement.Modules.EventManagement.Shared;

public static class CulturalEventExtensions
{
    public static WebApplicationBuilder AddCulturalEventModule(this WebApplicationBuilder builder)
    {
        builder.AddCulturalEventBuilder()
                .AddCreateEventHandler()
                .AddReadEventsHandler()
                .AddReadEventByIdHandler();
                
        return builder;
    }
}
