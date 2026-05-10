using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;

public class GetProviderCatalogEnpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/marketplace/catalog", async (
            GetProviderCatalogRequest request, 
            IMediator mediator,
            IValidator<GetProviderCatalogRequest> validator
        ) =>
        {
            validator.ValidateAndThrow(request);
            var response = await mediator.SendAsync<GetProviderCatalogRequest, GetProviderCatalogResponse>(request);
            return Results.Ok(response);
        });
    }
}
