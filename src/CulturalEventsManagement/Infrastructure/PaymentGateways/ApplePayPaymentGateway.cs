using CulturalEventsManagement.Modules.OrderManagement.Shared;

namespace CulturalEventsManagement.Infrastructure.PaymentGateways;

public sealed class ApplePayPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
    {
        Console.WriteLine("Processing payment with Apple Pay...");
        await Task.Delay(1000); // Simulate payment processing delay
        Console.WriteLine("Payment processed successfully with Apple Pay.");
        return true;
    }
}
