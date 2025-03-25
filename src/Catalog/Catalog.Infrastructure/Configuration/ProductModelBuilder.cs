using Catalog.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Catalog.Infrastructure.Configuration;

public static class ProductModelBuilder
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.EntityId).SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.ObjectId));;
        });
    }
}