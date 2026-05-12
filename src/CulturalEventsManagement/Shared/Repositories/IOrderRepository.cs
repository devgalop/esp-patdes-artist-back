using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Shared.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
}
