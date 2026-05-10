namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public class WhatsappNotificationServiceWrapper(
    INotificationService notificationService
) : NotificationServiceWrapper(notificationService)
{
    public override async Task SendAsync(BaseNotificationRequest request)
    {
        Console.WriteLine($"Sending WhatsApp notification to recipient {request.Recipient} with message: {request.Message}");
        await Task.Delay(400); // Simulate WhatsApp sending delay
        Console.WriteLine("WhatsApp notification sent successfully.");
    }
}
