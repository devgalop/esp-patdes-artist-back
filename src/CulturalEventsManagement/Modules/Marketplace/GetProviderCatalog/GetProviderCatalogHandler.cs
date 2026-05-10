using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;
using CulturalEventsManagement.Shared.Abstractions;

namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;



public sealed record GetProviderCatalogResponse(
    CatalogResponse Catalog
);

public sealed class GetProviderCatalogHandler(
    ICatalogService catalogService
) : IQueryHandler<GetProviderCatalogRequest, GetProviderCatalogResponse>
{
    public async Task<GetProviderCatalogResponse> HandleAsync(GetProviderCatalogRequest query)
    {
        Console.WriteLine($"Received request to get catalog for provider with id '{query.ProviderId}'.");
        var catalog = await catalogService.GetCatalogByProviderAsync(query.ProviderId);

        Console.WriteLine($"Catalog for provider with id '{query.ProviderId}' retrieved successfully.");
        return new GetProviderCatalogResponse(catalog);
    }
}

public static class GetProviderCatalogHandlerExtensions
{
    public static WebApplicationBuilder AddGetProviderCatalogHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IQueryHandler<GetProviderCatalogRequest, GetProviderCatalogResponse>, GetProviderCatalogHandler>();

        return builder;
    }
}
