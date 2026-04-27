namespace CulturalEventsManagement.Shared.Domain;

public enum EventType
{
    Concert,
    Exhibition,
    Theater,
    Festival,
    Workshop,
    Other,
    Unassigned
}

public enum EventStatus
{
    Scheduled,
    Ongoing,
    Completed,
    Canceled,
    Unassigned
}

public enum EventBillingType
{
    Free,
    Paid,
    Donation,
    Unassigned
}

public class CulturalEvent
{
    public Guid Id { get; private set; }
    public string Name { get;  private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public EventType Type { get; private set; }
    public EventStatus Status { get; private set; }
    public EventBillingType BillingType { get; private set; }

    public decimal Price { get; private set;}
    public string Location { get; private set; }


    public CulturalEvent()
    {
        Id = Guid.CreateVersion7();
        Name = string.Empty;
        Description = string.Empty;
        CreatedAt = DateTime.UtcNow;
        Type = EventType.Unassigned;
        Status = EventStatus.Unassigned;
        BillingType = EventBillingType.Unassigned;
        Price = 0;
        Location = string.Empty;
    }

    public void AssignId(Guid id)
    {
        Id = id;
    }

    public void AssignDate(DateTime scheduledAt)
    {
        ScheduledAt = scheduledAt;
        Status = EventStatus.Scheduled;
    }

    public void UpdateStatus(EventStatus newStatus)
    {
        Status = newStatus;
    }

    public void UpdateType(EventType newType)
    {
        Type = newType;
    }

    public void UpdateBillingType(EventBillingType newBillingType)
    {
        BillingType = newBillingType;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void AssignPrice(decimal price)
    {
        Price = price;
    }

    public void AssignLocation(string location)
    {
        Location = location;
    }
    
}
