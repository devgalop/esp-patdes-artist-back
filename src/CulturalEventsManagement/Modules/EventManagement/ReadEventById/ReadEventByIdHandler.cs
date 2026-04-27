using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Repositories;

namespace CulturalEventsManagement.Modules.EventManagement.ReadEventById;

public class ReadEventByIdHandler (
    ICulturalEventRepository repository
): IQueryHandler<ReadEventByIdRequest, ReadEventByIdResponse>
{
    public async Task<ReadEventByIdResponse> HandleAsync(ReadEventByIdRequest query)
    {
        var eventFound = await repository.GetByIdAsync(Guid.Parse(query.EventId));
        if (eventFound == null)
        {
            return new ReadEventByIdResponse(
                IsSuccess: false,
                Message: "El evento no existe o no se pudo encontrar dentro de la base de datos",
                Event: null
            );
        }
        var eventDetail = new CulturalEventDetail(
            Id: eventFound.Id,
            Name: eventFound.Name,
            Description: eventFound.Description,
            CreatedAt: eventFound.CreatedAt,
            ScheduledAt: eventFound.ScheduledAt,
            Type: eventFound.Type.ToString(),
            Status: eventFound.Status.ToString(),
            BillingType: eventFound.BillingType.ToString(),
            Price: eventFound.Price,
            Location: eventFound.Location
        );
        return new ReadEventByIdResponse(
            IsSuccess: true,
            Message: "Evento recuperado exitosamente",
            Event: eventDetail
        );
    }
}

public static class ReadEventByIdHandlerExtensions
{
    public static WebApplicationBuilder AddReadEventByIdHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<ReadEventByIdRequest, ReadEventByIdResponse>, ReadEventByIdHandler>();
        return builder;
    }
}
