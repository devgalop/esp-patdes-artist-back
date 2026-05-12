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
        Console.WriteLine($"Sending email notification to {request.Recipient} with message: {request.Message}");
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
