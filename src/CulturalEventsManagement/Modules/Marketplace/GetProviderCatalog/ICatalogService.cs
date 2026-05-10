namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog.Shared;

public interface ICatalogService
{
    Task<CatalogResponse> GetCatalogByProviderAsync(string providerId);
}
