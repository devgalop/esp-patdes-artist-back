namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public record BaseNotificationRequest(
    string Sender,
    string Recipient,
    string Message
);

public interface INotificationService
{
    Task SendAsync(BaseNotificationRequest request);
}
