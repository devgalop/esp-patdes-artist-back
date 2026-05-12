namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public class SmsNotificationServiceWrapper(
    INotificationService notificationService
) : NotificationServiceWrapper(notificationService)
{
    public override async Task SendAsync(BaseNotificationRequest request)
    {
        Console.WriteLine($"Sending SMS notification to recipient {request.Recipient} with message: {request.Message}");
        await Task.Delay(300); // Simulate SMS sending delay
        Console.WriteLine("SMS notification sent successfully.");
        await base.SendAsync(request);
    }
}

