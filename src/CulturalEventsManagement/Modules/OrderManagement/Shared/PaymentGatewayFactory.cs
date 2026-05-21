using CulturalEventsManagement.Infrastructure.PaymentGateways;

namespace CulturalEventsManagement.Modules.OrderManagement.Shared;

public interface IPaymentGatewayFactory
{
    IPaymentGateway Create(PaymentMethod paymentMethod);
}

public sealed class PaymentGatewayFactory : IPaymentGatewayFactory
{
    public IPaymentGateway Create(PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.CreditCard => new CreditCardPaymentGateway(),
            PaymentMethod.PayPal => new PayPalPaymentGateway(),
            PaymentMethod.BankTransfer => new BankTransferPaymentGateway(),
            PaymentMethod.ApplePay => new ApplePayPaymentGateway(),
            PaymentMethod.GooglePay => new GooglePayPaymentGateway(),
            PaymentMethod.PSE => new PsePaymentGateway(),
            _ => throw new NotSupportedException($"Payment method {paymentMethod} is not supported.")
        };
    }
}

public static class PaymentGatewayFactoryExtensions
{
    public static WebApplicationBuilder AddPaymentGatewayFactory(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPaymentGatewayFactory, PaymentGatewayFactory>();
        return builder;
    }
}
