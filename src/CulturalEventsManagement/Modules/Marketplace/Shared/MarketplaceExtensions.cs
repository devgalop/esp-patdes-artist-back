using CulturalEventsManagement.Infrastructure.ProductCatalogs;
using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;
using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

namespace CulturalEventsManagement.Modules.Marketplace.Shared;

public static class MarketplaceExtensions
{
    public static WebApplicationBuilder AddMarketplaceModule(this WebApplicationBuilder builder)
    {
        builder.AddProductCatalogs()
                .AddGetCatalogByProviderService()
                .AddGetCatalogByProviderCache()
                .AddGetProviderCatalogHandler();

        return builder;
    }
}
