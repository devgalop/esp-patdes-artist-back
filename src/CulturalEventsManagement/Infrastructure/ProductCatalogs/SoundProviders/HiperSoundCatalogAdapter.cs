using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.SoundProviders;

public class HiperSoundCatalogAdapter(
    HiperSoundCatalogService service
) : IProviderCatalogService
{
    public async Task<Modules.Marketplace.GetProviderCatalog.Shared.CatalogResponse> GetProductCatalogAsync()
    {
        var soundCatalog = await service.GetProductsAsync();

        var items = soundCatalog.Products.Select(item => new CatalogItem(
            Id: item.Identifier,
            Name: item.Title,
            Description: item.Details,
            Price: item.Cost
        ));

        return new Modules.Marketplace.GetProviderCatalog.Shared.CatalogResponse(
            ProviderId: soundCatalog.ProviderCode,
            Items: items
        );
    }
}

public static class HiperSoundExtensions
{
    public static WebApplicationBuilder AddHiperSoundProvider(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<HiperSoundCatalogService>();
        builder.Services.AddKeyedSingleton<IProviderCatalogService, HiperSoundCatalogAdapter>("HiperSound");

        return builder;
    }
}
