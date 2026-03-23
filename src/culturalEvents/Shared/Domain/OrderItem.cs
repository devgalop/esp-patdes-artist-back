namespace culturalEvents.Shared.Domain
{
    public sealed class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Quantity * (Offering?.Price ?? 0);

        public Guid? OrderId { get; set; }

        public Guid? OfferingId { get; set; }
        public Offering? Offering { get; set; }

        public OrderItem()
        {
            Id = Guid.CreateVersion7();
            Quantity = 0;
            OfferingId = null;
            Offering = null;
            OrderId = null;
        }

        public OrderItem(Guid orderId, Offering offering, int quantity)
        {
            Id = Guid.CreateVersion7();
            OrderId = orderId;
            Offering = offering;
            OfferingId = offering.Id;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void IncrementQuantity(int quantity)
        {
            Quantity += quantity;
        }

    }
}