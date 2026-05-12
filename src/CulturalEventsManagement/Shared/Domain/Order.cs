namespace CulturalEventsManagement.Shared.Domain;

public class Order(
    string userId,
    Guid eventId,
    string providerId,
    string cuponCode,
    string observations,
    List<OrderItemDetail> details
)
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public string UserId { get; private set; } = userId;
    public Guid EventId { get; private set; } = eventId;
    public string ProviderId { get; private set; } = providerId;
    public string CuponCode { get; private set; } = cuponCode;
    public string Observations { get; private set; } = observations;
    public List<OrderItemDetail> Details { get; private set; } = details;
}

public class OrderItemDetail(string productId, int quantity)
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public string ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;
}
