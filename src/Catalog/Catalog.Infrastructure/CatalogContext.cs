using Catalog.Domain;
using Catalog.Infrastructure.Interfaces;
using Catalog.Infrastructure.Settings;
using MongoDB.Driver;

namespace Catalog.Infrastructure;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }

    public CatalogContext(ICatalogDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        Products = database.GetCollection<Product>(databaseSettings.CollectionName);

        CatalogContextSeed.SeedData(Products);
    }
}