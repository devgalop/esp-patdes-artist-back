namespace culturalEvents.Modules.EventManagement.Common;

public static class EventCommonExtensions
{
    public static WebApplicationBuilder AddEventManagementModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventBuilder, EventBuilder>();
        return builder;
    }

}
