using Discount.Infrastructure.Interfaces;
using Discount.Infrastructure.Settings;
using MongoDB.Driver;

namespace Discount.Infrastructure;

public class DiscountContext : IDiscountContext
{
    public IMongoCollection<Domain.Discount> Discounts { get; }

    public DiscountContext(IDiscountDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        Discounts = database.GetCollection<Domain.Discount>(databaseSettings.CollectionName);

        DiscountContextSeed.SeedData(Discounts);
    }
}