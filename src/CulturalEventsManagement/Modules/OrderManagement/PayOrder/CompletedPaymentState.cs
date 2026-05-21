using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class CompletedPaymentState : IPaymentState
{
    private PaymentOrderContext _context;

    public async Task Cancel()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Cannot cancel a completed order.");
        _context.SetState(new CompletedPaymentState());
    }

    public async Task DoNext()
    {
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Order is already completed.");
        _context.SetState(new CompletedPaymentState());
    }

    public OrderStatus GetOrderStatus()
    {
        return OrderStatus.Completed;
    }

    public void SetContext(PaymentOrderContext context)
    {
        _context = context;
    }
}
