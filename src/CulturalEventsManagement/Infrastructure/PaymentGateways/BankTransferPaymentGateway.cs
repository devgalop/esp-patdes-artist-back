using CulturalEventsManagement.Modules.OrderManagement.Shared;

namespace CulturalEventsManagement.Infrastructure.PaymentGateways;

public sealed class BankTransferPaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPaymentAsync(PaymentRequest request)
    {
        Console.WriteLine("Processing payment with Bank Transfer...");
        await Task.Delay(1000); // Simulate payment processing delay
        Console.WriteLine("Payment processed successfully with Bank Transfer.");
        return true;
    }
}
