namespace CulturalEventsManagement.Infrastructure.Database.Shared.Models;

public class OrderEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string ProviderId { get; set; } = string.Empty;
    public string CuponCode { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;

    public ICollection<OrderDetailEntity> OrderDetails { get; set; } = new List<OrderDetailEntity>();
}
