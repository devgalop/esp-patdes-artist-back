namespace CulturalEventsManagement.Modules.OrderManagement.Shared;

public enum PaymentMethod
{
    CreditCard,
    PayPal,
    BankTransfer,
    ApplePay,
    GooglePay,
    PSE
}

public sealed record PaymentRequest(
    string OrderId,
    PaymentMethod PaymentMethod
);

public interface IPaymentGateway
{
    Task<bool> ProcessPaymentAsync(PaymentRequest request);
}
