using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Modules.OrderManagement.PayOrder;

public interface IPaymentState
{
    void SetContext(PaymentOrderContext context);

    Task DoNext();

    Task Cancel();

    OrderStatus GetOrderStatus();
}
