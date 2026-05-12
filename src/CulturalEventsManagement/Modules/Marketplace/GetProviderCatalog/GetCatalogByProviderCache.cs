using System.Collections.Concurrent;
using CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;

public sealed record CacheCatalogResponse(
    string ProviderId,
    CatalogResponse Catalog,
    DateTime CachedAt
);

public sealed class GetCatalogByProviderCache(
    GetCatalogByProviderService catalogService
) : ICatalogService
{
    private readonly ConcurrentDictionary<string, CacheCatalogResponse> _cache = new();
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public async Task<CatalogResponse> GetCatalogByProviderAsync(string providerId)
    {
        if (_cache.TryGetValue(providerId, out var cachedResponse) 
        && (DateTime.UtcNow - cachedResponse.CachedAt) < _cacheDuration)
        {
            return cachedResponse.Catalog;
        }

        var response = await catalogService.GetCatalogByProviderAsync(providerId);
        var cacheResponse = new CacheCatalogResponse(providerId, response, DateTime.UtcNow);
        _cache[providerId] = cacheResponse;

        return response;
    }
}

public static class GetCatalogByProviderCacheExtensions
{
    public static WebApplicationBuilder AddGetCatalogByProviderCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICatalogService, GetCatalogByProviderCache>();

        return builder;
    }
}
