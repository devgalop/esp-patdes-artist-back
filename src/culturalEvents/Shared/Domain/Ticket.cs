namespace culturalEvents.Shared.Domain
{
    public sealed class Ticket
    {
        public Guid Id { get; private set; }
        public DateTime UtcReservationDate { get; set; }
        public Guid? EventId { get; set; }
        public CulturalEvent? Event { get; set; }

        public Guid? TicketSeatId { get; set; }
        public TicketSeat? TicketSeat { get; set; }

        public Ticket()
        {
            Id = Guid.CreateVersion7();
            EventId = null;
            Event = null;
            TicketSeat = null;
            TicketSeatId = null;
            UtcReservationDate = DateTime.UtcNow;
        }

        public Ticket(CulturalEvent evnt, TicketSeat ticketSeat)
        {
            Id = Guid.CreateVersion7();
            Event = evnt;
            EventId = evnt.Id;
            TicketSeat = ticketSeat;
            TicketSeatId = ticketSeat.Id;
            UtcReservationDate = DateTime.UtcNow;
        }
    }
}