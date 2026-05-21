using CulturalEventsManagement.Modules.OrderManagement.Shared;
using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public sealed class PaymentOrderContext(
    Order order,
    IPaymentGateway paymentGateway,
    PaymentMethod paymentMethod
)
{
    private IPaymentState _state;

    public void SetState(IPaymentState state)
    {
        _state = state;
        order.UpdateStatus(_state.GetOrderStatus());
        _state.SetContext(this);
    }

    public Task DoNext()
    {
        return _state.DoNext();
    }

    public Task Cancel()
    {
        return _state.Cancel();
    }

    public async Task<bool> ProcessPaymentAsync()
    {
        var result = await paymentGateway.ProcessPaymentAsync(new PaymentRequest(
            order.Id.ToString(),
            paymentMethod
        ));
        return result;
    }
    
}
