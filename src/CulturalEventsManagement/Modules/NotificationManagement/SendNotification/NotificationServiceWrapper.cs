namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public abstract class NotificationServiceWrapper(
    INotificationService notificationService
) : INotificationService
{    
    public virtual async Task SendAsync(BaseNotificationRequest request)
    {
        await notificationService.SendAsync(request);
    }
}
