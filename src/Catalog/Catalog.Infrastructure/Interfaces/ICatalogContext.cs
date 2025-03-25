using Catalog.Domain;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Interfaces;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}