using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;

public class GetProviderCatalogEnpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/marketplace/catalog", async (
            string providerId, 
            IMediator mediator,
            IValidator<GetProviderCatalogRequest> validator
        ) =>
        {
            GetProviderCatalogRequest request = new(providerId);
            validator.ValidateAndThrow(request);
            var response = await mediator.SendAsync<GetProviderCatalogRequest, GetProviderCatalogResponse>(request);
            return Results.Ok(response);
        });
    }
}
