namespace CulturalEventsManagement.Modules.NotificationManagement.SendNotification;

public sealed record EmailNotificationRequest(
    string Title,
    string Sender,
    string Recipient,
    string Message
) : BaseNotificationRequest(Sender, Recipient, Message);

public sealed class EmailNotificationService : INotificationService
{
    public async Task SendAsync(BaseNotificationRequest request)
    {
        EmailNotificationRequest req = (EmailNotificationRequest)request;
        Console.WriteLine($"Sending email notification to {req.Recipient} with title '{req.Title}' and message: {req.Message}");
        await Task.Delay(500); // Simulate email sending delay
        Console.WriteLine("Email notification sent successfully.");
    }
}

public static class EmailNotificationServiceExtensions
{
    public static WebApplicationBuilder AddEmailNotificationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<INotificationService, EmailNotificationService>();
        return builder;
    }
}
