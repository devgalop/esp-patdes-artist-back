
namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

public sealed class GetCatalogByProviderService(
    IEnumerable<IProviderCatalogService> providerCatalogServices
): ICatalogService
{
    private readonly Dictionary<string, IProviderCatalogService> serviceProvider = 
    providerCatalogServices.ToDictionary(s => s.GetProviderId(), s => s);

    public async Task<CatalogResponse> GetCatalogByProviderAsync(string providerId)
    {
        if(!serviceProvider.TryGetValue(providerId, out var serviceSelected))
        {
            throw new ArgumentException($"Provider with id '{providerId}' not found.");
        }
        return await serviceSelected.GetProductCatalogAsync();
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
