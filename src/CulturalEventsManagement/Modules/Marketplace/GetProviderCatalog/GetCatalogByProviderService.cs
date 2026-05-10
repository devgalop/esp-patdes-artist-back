
namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

public sealed class GetCatalogByProviderService(
    IServiceProvider serviceProvider
): ICatalogService
{
    public async Task<CatalogResponse> GetCatalogByProviderAsync(string providerId)
    {
        return providerId switch
        {
            "HomeForniture" => await serviceProvider
                                        .GetRequiredKeyedService<IProviderCatalogService>("HomeForniture")
                                        .GetProductCatalogAsync(),
            "HiperSound" => await serviceProvider
                                        .GetRequiredKeyedService<IProviderCatalogService>("HiperSound")
                                        .GetProductCatalogAsync(),
            "MultiSound" => await serviceProvider
                                        .GetRequiredKeyedService<IProviderCatalogService>("MultiSound")
                                        .GetProductCatalogAsync(),
            _ => throw new ArgumentException($"Provider with id '{providerId}' not found.")
        };
    }
}

public static class GetCatalogByProviderExtensions
{
    public static WebApplicationBuilder AddGetCatalogByProviderService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICatalogService, GetCatalogByProviderService>();

        return builder;
    }
}
