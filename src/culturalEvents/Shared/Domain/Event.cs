namespace culturalEvents.Shared.Domain
{
    public enum EventCategory
    {
        MUSIC,
        THEATER,
        DANCE,
        EXHIBITION,
        WORKSHOP,
        OTHER
    }

    public enum EventStatus
    {
        PENDING,
        SCHEDULED,
        CANCELLED,
        POSTPONED,
        COMPLETED
    }

    public enum EventValue
    {
        FREE,
        PAID
    }

    public sealed class CulturalEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public DateTime UtcDate { get; set; }
        public EventCategory Category { get; set; }
        public EventStatus Status { get; set; }
        public EventValue Value { get; set; }
        
        public Guid? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public Guid? ArtistId { get; set; }
        public User? Artist { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public CulturalEvent()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Category = EventCategory.OTHER;
            Status = EventStatus.PENDING;
            Value = EventValue.FREE;
            VenueId = null;
            Venue = null;
            ArtistId = null;
            Artist = null;
            Orders = new List<Order>();
            Tickets = new List<Ticket>();
        }

        public CulturalEvent(string name, DateTime eventDate, Venue venue, User artist)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            UtcDate = eventDate;
            Category = EventCategory.OTHER;
            Status = EventStatus.PENDING;
            Value = EventValue.FREE;
            Venue = venue;
            VenueId = venue.Id;
            ArtistId = artist.Id;
            Artist = artist;
            Orders = new List<Order>();
            Tickets = new List<Ticket>();
        }

        public void UpdateCategory(EventCategory category)
        {
            Category = category;
        }

        public void UpdateStatus(EventStatus status)
        {
            Status = status;
        }

        public void UpdateValue(EventValue value)
        {
            Value = value;
        }

        public void PostponeEvent(DateTime newDate)
        {
            UtcDate = newDate;
            Status = EventStatus.POSTPONED;
        }

        public void CancelEvent()
        {
            Status = EventStatus.CANCELLED;
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

    }
}