using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class CancelledPaymentState : IPaymentState
{
    private PaymentOrderContext _context;
    public async Task Cancel()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Order is already cancelled.");
        _context.SetState(new CancelledPaymentState());
    }

    public async Task DoNext()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Order is already cancelled.");
        _context.SetState(new CancelledPaymentState());
    }

    public OrderStatus GetOrderStatus()
    {
        return OrderStatus.Cancelled;
    }

    public void SetContext(PaymentOrderContext context)
    {
        _context = context;
    }
}
