using CulturalEventsManagement.Infrastructure.Database.Shared;
using CulturalEventsManagement.Shared.Domain;
using CulturalEventsManagement.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CulturalEventsManagement.Infrastructure.Database.OrderManagement;

public sealed class OrderRepository(
    AppDatabaseContext dbContext,
    OrderMapper mapper
) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        var entity = mapper.ToEntity(order);
        dbContext.Orders.Add(entity);

        foreach (var detail in order.Details)
        {
            var detailEntity = mapper.ToDetailEntity(detail, entity.Id);
            dbContext.OrderDetails.Add(detailEntity);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var entity = await dbContext.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id.ToString());

        if (entity == null)
        {
            return null;
        }

        return mapper.ToDomain(entity);
    }
}

public static class OrderRepositoryExtensions
{
    public static WebApplicationBuilder AddOrderRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        return builder;
    }
}
