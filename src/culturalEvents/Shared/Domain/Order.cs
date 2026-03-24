namespace culturalEvents.Shared.Domain
{
    public sealed class Order
    {
        public Guid Id { get; set; }
        public DateTime UtcCreatedAt { get; private set; }

        public Guid? ArtistId { get; set; }
        public User? Artist { get; set; }

        public Guid? ProviderId { get; set; }
        public User? Provider { get; set; }

        public Guid? EventId { get; set; }
        public CulturalEvent? Event { get; set; }

        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Id = Guid.CreateVersion7();
            UtcCreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
            ArtistId = null;
            ProviderId = null;
            Artist = null;
            Provider = null;
            EventId = null;
            Event = null;
        }

        public Order(User artist, User provider, CulturalEvent culturalEvent)
        {
            Id = Guid.CreateVersion7();
            UtcCreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
            ArtistId = artist.Id;
            Artist = artist;
            ProviderId = provider.Id;
            Provider = provider;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            Items.AddRange(items);
        }

        public decimal GetTotal()
        {
            return Items.Sum(item => item.GetTotal());
        }
    }
}