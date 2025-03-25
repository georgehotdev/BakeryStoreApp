using Catalog.Domain;
using MongoDB.Driver;

namespace Catalog.Infrastructure;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        var productsAlreadySeeded = productCollection.Find(p => true).Any();
        if (!productsAlreadySeeded)
        {
            productCollection.InsertMany(GetSeedData());
        }
    }

    private static IEnumerable<Product> GetSeedData()
    {
        return new List<Product>
        {
            new()
            {
                Id = 1,
                Name = "Brownie",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/68/Chocolatebrownie.JPG/715px-Chocolatebrownie.JPG",
                Price = 2.0m
            },
            new()
            {
                Id = 2,
                Name = "Key Lime Cheesecake",
                ImageUrl =
                    "http://1.bp.blogspot.com/-7we9Z0C_fpI/T90JXcg3YsI/AAAAAAAABn4/EN7u2vMuRug/s1600/key+lime+cheesecake+slice+in+front.jpg",
                Price = 8.0m
            },
            new()
            {
                Id = 3,
                Name = "Cookie",
                ImageUrl = "http://www.mayheminthekitchen.com/wp-content/uploads/2015/05/chocolate-cookie-square.jpg",
                Price = 6.0m
            },
            new()
            {
                Id = 4,
                Name = "Mini Gingerbread Donut",
                ImageUrl = "https://allylazare.com/wp-content/uploads/2022/06/gingerbread-donut-with-cinnamon-glaze-1-800x530.webp",
                Price = 0.5m
            },
        };
    }
}