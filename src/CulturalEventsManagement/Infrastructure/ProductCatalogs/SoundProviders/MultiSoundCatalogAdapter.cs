using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.SoundProviders;

public class MultiSoundCatalogAdapter(
    MultiSoundCatalogService service
) : IProviderCatalogService
{
    public string GetProviderId() => "MultiSound";
    public async Task<Modules.Marketplace.GetProviderCatalog.Shared.CatalogResponse> GetProductCatalogAsync()
    {
        var soundCatalog = await service.GetCatalogAsync();

        var items = soundCatalog.Items.Select(item => new CatalogItem(
            Id: item.ProductId,
            Name: item.Name,
            Description: item.Description,
            Price: item.Price
        ));

        return new Modules.Marketplace.GetProviderCatalog.Shared.CatalogResponse(
            ProviderId: soundCatalog.Id,
            Items: items
        );
    }
}

public static class MultiSoundExtensions
{
    public static WebApplicationBuilder AddMultiSoundProvider(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<MultiSoundCatalogService>();
        builder.Services.AddSingleton<IProviderCatalogService, MultiSoundCatalogAdapter>();

        return builder;
    }
}
