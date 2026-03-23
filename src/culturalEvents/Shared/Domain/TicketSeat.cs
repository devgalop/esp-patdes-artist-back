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

        public TicketSeat()
        {
            Id = Guid.CreateVersion7();
            SeatNumber = string.Empty;
            Status = TicketSeatStatus.AVAILABLE;
        }

        public TicketSeat(string seatNumber)
        {
            Id = Guid.CreateVersion7();
            SeatNumber = seatNumber;
            Status = TicketSeatStatus.AVAILABLE;
        }

        public void MarkAsSold()
        {
            Status = TicketSeatStatus.SOLD;
        }
    }
}