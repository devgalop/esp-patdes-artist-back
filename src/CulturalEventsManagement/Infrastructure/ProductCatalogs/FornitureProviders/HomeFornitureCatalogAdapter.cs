using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.FornitureProviders;

public class HomeFornitureCatalogAdapter(
    HomeFornitureCatalogService service
) : IProviderCatalogService
{
    public async Task<CatalogResponse> GetProductCatalogAsync()
    {
        var catalogs = await service.GetCatalogAsync();
        IEnumerable<CatalogItem> items = catalogs.Select(c => new CatalogItem(
            Id: c.Name,
            Name: c.Name,
            Description: c.Description,
            Price: c.Price
        ));
        string providerId = catalogs.FirstOrDefault()?.Provider ?? "HomeForniture";

        return new CatalogResponse(
            ProviderId: providerId,
            Items: items
        );
    }
}

public static class HomeFornitureExtensions
{
    public static WebApplicationBuilder AddHomeFornitureProvider(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<HomeFornitureCatalogService>();
        builder.Services.AddKeyedSingleton<IProviderCatalogService, HomeFornitureCatalogAdapter>("HomeForniture");

        return builder;
    }
}
