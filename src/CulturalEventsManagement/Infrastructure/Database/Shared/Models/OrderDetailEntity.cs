namespace CulturalEventsManagement.Infrastructure.Database.Shared.Models;

public class OrderDetailEntity
{
    public string Id { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public string CatalogItemId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
