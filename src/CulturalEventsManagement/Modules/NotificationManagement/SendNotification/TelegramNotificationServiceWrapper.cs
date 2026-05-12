namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public class TelegramNotificationServiceWrapper(
    INotificationService notificationService
) : NotificationServiceWrapper(notificationService)
{
    public override async Task SendAsync(BaseNotificationRequest request)
    {
        Console.WriteLine($"Sending Telegram notification to recipient {request.Recipient} with message: {request.Message}");
        await Task.Delay(350); // Simulate Telegram sending delay
        Console.WriteLine("Telegram notification sent successfully.");
        await base.SendAsync(request);
    }
}
