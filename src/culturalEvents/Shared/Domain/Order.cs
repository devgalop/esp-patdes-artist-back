namespace culturalEvents.Shared.Domain
{
    public sealed class Order
    {
        public Guid Id { get; set; }
        public DateTime UtcCreatedAt { get; private set; }
        public decimal Total => Items.Sum(item => item.Total);

        public Guid? ArtistId { get; set; }
        public Guid? ProviderId { get; set; }

        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Id = Guid.CreateVersion7();
            UtcCreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
            ArtistId = null;
            ProviderId = null;
        }

        public Order(User artist, User provider)
        {
            Id = Guid.CreateVersion7();
            UtcCreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
            ArtistId = artist.Id;
            ProviderId = provider.Id;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            Items.AddRange(items);
        }
    }
}