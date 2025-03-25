using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string EntityId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}