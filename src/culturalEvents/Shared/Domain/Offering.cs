using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Domain
{
    public enum OfferingStatus
    {
        AVAILABLE = 1,
        UNAVAILABLE = 0,
        DISCONTINUED = -1
    }

    public sealed class Offering
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public OfferingStatus Status { get; set; }

        public Guid? ProviderId { get; set; }
        public User? Provider { get; set; }

        public Offering()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Description = string.Empty;
            Price = 0;
            ProviderId = null;
            Provider = null;
            Status = OfferingStatus.AVAILABLE;
        }

        public Offering(string name, string description, decimal price, User provider)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Price = price;
            Provider = provider;
            ProviderId = provider.Id;
            Status = OfferingStatus.AVAILABLE;
        }

        public void UpdateStatus(OfferingStatus newStatus)
        {
            Status = newStatus;
        }
    }
}