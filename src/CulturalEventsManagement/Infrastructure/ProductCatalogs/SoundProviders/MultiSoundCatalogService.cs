namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.SoundProviders;

public record SoundCatalogResponse(
    string Id,
    IEnumerable<SoundCatalogItem> Items
);

public record SoundCatalogItem(
    string ProductId,
    string Name,
    string Description,
    decimal Price
);

public sealed class MultiSoundCatalogService
{
    public async Task<SoundCatalogResponse> GetCatalogAsync()
    {
        // Simulate fetching data from multiple sound providers and aggregating it
        Console.WriteLine("Fetching Multi Sound Catalog...");
        await Task.Delay(100); // Simulate API Consume Delay

        var items = new List<SoundCatalogItem>
        {
            new SoundCatalogItem("sound1", "Sound Effect 1", "High-quality sound effect", 9.99m),
            new SoundCatalogItem("sound2", "Sound Effect 2", "Another great sound effect", 14.99m)
        };

        return new SoundCatalogResponse("multi-sound-provider", items);
    }
}
