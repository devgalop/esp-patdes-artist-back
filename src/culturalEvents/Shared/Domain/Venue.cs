namespace culturalEvents.Shared.Domain
{
    public enum VenueStatus
    {
        AVAILABLE = 1,
        UNAVAILABLE = 0,
        UNDER_MAINTENANCE = -1
    }

    public sealed class Venue
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public VenueStatus Status { get; set; }
        public decimal RentalPrice { get; set; }

        public ICollection<CulturalEvent> Events { get; set; }
        public ICollection<TicketSeat> Seats { get; set; }

        public Venue()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Address = string.Empty;
            Capacity = 0;
            Status = VenueStatus.AVAILABLE;
            RentalPrice = 0;
            Events = new List<CulturalEvent>();
            Seats = new List<TicketSeat>();
        }

        public Venue(string name, string address, int capacity, decimal rentalPrice)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Address = address;
            Capacity = capacity;
            RentalPrice = rentalPrice;
            Status = VenueStatus.AVAILABLE;
            Events = new List<CulturalEvent>();
            Seats = new List<TicketSeat>();
        }

        public void UpdateStatus(VenueStatus status)
        {
            Status = status;
        }
    }
}