using CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

namespace CulturalEventsManagement.Modules.NotificationManagement;

public sealed record UserNotificationConfig(
    string UserId,
    bool SmsEnabled,
    bool TelegramEnabled,
    bool WhatsappEnabled
);

public sealed class NotificationOrchestrator(
    INotificationService notificationService
)
{
    public async Task NotificateAsync(
        UserNotificationConfig config,
        BaseNotificationRequest request
    )
    {
        INotificationService chainedNotificationService = notificationService;
        if (config.SmsEnabled)
        {
            chainedNotificationService = new SmsNotificationServiceWrapper(chainedNotificationService);
        }
        if (config.TelegramEnabled)
        {
            chainedNotificationService = new TelegramNotificationServiceWrapper(chainedNotificationService);
        }
        if (config.WhatsappEnabled)
        {
            chainedNotificationService = new WhatsappNotificationServiceWrapper(chainedNotificationService);
        }
        await chainedNotificationService.SendAsync(request);
    }
}

public static class NotificationOrchestratorExtensions
{
    public static WebApplicationBuilder AddNotificationOrchestrator(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<NotificationOrchestrator>();
        return builder;
    }
}

