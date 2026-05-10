using CulturalEventsManagement.Infrastructure.ProductCatalogs.FornitureProviders;
using CulturalEventsManagement.Infrastructure.ProductCatalogs.SoundProviders;

namespace CulturalEventsManagement.Infrastructure.ProductCatalogs;

public static class ProductCatalogExtensions
{
    public static WebApplicationBuilder AddProductCatalogs(this WebApplicationBuilder builder)
    {
        builder.AddHomeFornitureProvider();
        builder.AddHiperSoundProvider();
        builder.AddMultiSoundProvider();

        return builder;
    }
}
