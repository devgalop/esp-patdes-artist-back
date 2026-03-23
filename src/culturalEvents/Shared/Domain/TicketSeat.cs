namespace culturalEvents.Shared.Domain
{
    public enum TicketSeatStatus
    {
        AVAILABLE = 1,
        SOLD = 0
    }

    public sealed class TicketSeat
    {
        public Guid Id { get; private set; }
        public string SeatNumber { get; set; }
        public TicketSeatStatus Status { get; set; }

        public Guid? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public TicketSeat()
        {
            Id = Guid.CreateVersion7();
            SeatNumber = string.Empty;
            Status = TicketSeatStatus.AVAILABLE;
            VenueId = null;
            Venue = null;
        }

        public TicketSeat(Venue venue, string seatNumber)
        {
            Id = Guid.CreateVersion7();
            SeatNumber = seatNumber;
            Status = TicketSeatStatus.AVAILABLE;
            VenueId = venue.Id;
            Venue = venue;
        }

        public void MarkAsSold()
        {
            Status = TicketSeatStatus.SOLD;
        }
    }
}