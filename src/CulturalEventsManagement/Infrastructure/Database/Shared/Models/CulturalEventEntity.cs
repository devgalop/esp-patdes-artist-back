namespace CulturalEventsManagement.Infrastructure.Database.Shared.Models;

public class CulturalEventEntity
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ScheduledAt { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string BillingType { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public string Location { get; set; } = string.Empty;
}
