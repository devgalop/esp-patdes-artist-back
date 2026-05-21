using CulturalEventsManagement.Modules.OrderManagement.Shared;

namespace CulturalEventsManagement.Infrastructure.PaymentGateways;

public sealed class CreditCardPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
    {
        Console.WriteLine("Processing payment with Credit Card...");
        await Task.Delay(500); // Simulate payment processing delay
        Console.WriteLine("Payment processed successfully with Credit Card.");
        return true;
    }
}
