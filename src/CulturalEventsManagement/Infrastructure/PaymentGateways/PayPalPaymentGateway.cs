using CulturalEventsManagement.Modules.OrderManagement.Shared;

namespace CulturalEventsManagement.Infrastructure.PaymentGateways;

public sealed class PayPalPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
    {
        Console.WriteLine("Processing payment with PayPal...");
        await Task.Delay(1000); // Simulate payment processing delay
        Console.WriteLine("Payment processed successfully with PayPal.");
        return true;
    }
}
