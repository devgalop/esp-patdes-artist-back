namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.SoundProviders;

public record CatalogResponse(
    string ProviderCode,
    IEnumerable<ProductItem> Products
);

public record ProductItem(
    string Identifier,
    string Title,
    string Details,
    decimal Cost
);

public class HiperSoundCatalogService
{
    public async Task<CatalogResponse> GetProductsAsync()
    {
        // Simulate fetching data from Hiper Sound provider
        Console.WriteLine("Fetching Hiper Sound Catalog...");
        await Task.Delay(100); // Simulate API Consume Delay

        var products = new List<ProductItem>
        {
            new ProductItem("hiper1", "Hiper Sound Effect 1", "High-quality sound effect", 9.99m),
            new ProductItem("hiper2", "Hiper Sound Effect 2", "Another great sound effect", 14.99m)
        };

        return new CatalogResponse("hiper-sound-provider", products);
    }
}
