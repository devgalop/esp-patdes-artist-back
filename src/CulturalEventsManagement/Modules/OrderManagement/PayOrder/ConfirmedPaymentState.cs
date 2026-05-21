using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class ConfirmedPaymentState : IPaymentState
{
    private PaymentOrderContext _context;
    public async Task Cancel()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Payment is already confirmed. Cannot cancel.");
        _context.SetState(new ConfirmedPaymentState());
    }

    public async Task DoNext()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Payment is already confirmed. No further actions possible.");
        _context.SetState(new ConfirmedPaymentState());
    }

    public OrderStatus GetOrderStatus()
    {
        return OrderStatus.Confirmed;
    }

    public void SetContext(PaymentOrderContext context)
    {
        _context = context;
    }
}
