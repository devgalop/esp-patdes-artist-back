namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

public sealed record CatalogResponse(
    string ProviderId,
    IEnumerable<CatalogItem> Items  
);

public sealed record CatalogItem(
    string Id,
    string Name,
    string Description,
    decimal Price
);

public interface IProviderCatalogService
{
    Task<CatalogResponse> GetProductCatalogAsync();
}
