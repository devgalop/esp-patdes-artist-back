namespace culturalEvents.Shared.Domain
{
    public sealed class OrderItem
    {
        public int Quantity { get; set; }
        public decimal Total => Quantity * (Offering?.Price ?? 0);

        public Guid? OfferingId { get; set; }
        public Offering? Offering { get; set; }

        public OrderItem()
        {
            Quantity = 0;
            OfferingId = null;
            Offering = null;
        }

        public OrderItem(Offering offering, int quantity)
        {
            Offering = offering;
            OfferingId = offering.Id;
            Quantity = quantity;
        }

    }
}