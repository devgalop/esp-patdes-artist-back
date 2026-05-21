using CulturalEventsManagement.Modules.OrderManagement.Shared;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class PendingPaymentState: IPaymentState
{
    private PaymentOrderContext _context;

    public void SetContext(PaymentOrderContext context)
    {
        _context = context;
    }

    public async Task DoNext()
    {
        Console.WriteLine("Processing payment...");
        // Simulate payment processing logic here
        await Task.Delay(1000); // Simulate async work
        var result = await _context.ProcessPaymentAsync();
        if (!result)
        {
            Console.WriteLine("Payment failed.");
            _context.SetState(new CancelledPaymentState());
            return;
        }
        Console.WriteLine("Payment processed successfully.");
        _context.SetState(new ConfirmedPaymentState());
    }

    public async Task Cancel()
    {
        Console.WriteLine("Cancelling order...");
        // Simulate cancellation logic here
        await Task.Delay(500); // Simulate async work
        Console.WriteLine("Order cancelled.");
        _context.SetState(new CancelledPaymentState());
    }

    public OrderStatus GetOrderStatus()
    {
        return OrderStatus.Pending;
    }
}
