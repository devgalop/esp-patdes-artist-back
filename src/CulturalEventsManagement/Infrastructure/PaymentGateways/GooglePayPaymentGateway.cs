using CulturalEventsManagement.Modules.OrderManagement.Shared;

namespace CulturalEventsManagement.Infrastructure.PaymentGateways;

public sealed class GooglePayPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
    {
        Console.WriteLine("Processing payment with Google Pay...");
        await Task.Delay(1000); // Simulate payment processing delay
        Console.WriteLine("Payment processed successfully with Google Pay.");
        return true;
    }
}
