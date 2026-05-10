namespace CulturalEventsManagement.Infrastructure.ProductCatalogs.FornitureProviders;

public record Catalog(
    string Provider,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    string ImageUrl
);

public sealed class HomeFornitureCatalogService
{
    public async Task<IEnumerable<Catalog>> GetCatalogAsync()
    {
        Console.WriteLine("Fetching HomeForniture catalog...");
        // Simulate fetching data from an external source
        await Task.Delay(100); // Simulate network delay

        Console.WriteLine("HomeForniture catalog fetched successfully.");

        return new List<Catalog>
        {
            new Catalog(
                Provider: "HomeForniture",
                Name: "Sofa",
                Description: "Comfortable 3-seater sofa",
                Price: 499.99m,
                Currency: "USD",
                ImageUrl: "https://example.com/sofa.jpg"
            ),
            new Catalog(
                Provider: "HomeForniture",
                Name: "Dining Table",
                Description: "Elegant dining table for 6 people",
                Price: 899.99m,
                Currency: "USD",
                ImageUrl: "https://example.com/dining_table.jpg"
            )
        };
    }
}
