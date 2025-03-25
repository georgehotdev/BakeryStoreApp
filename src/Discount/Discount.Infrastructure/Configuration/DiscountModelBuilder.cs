using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Discount.Infrastructure.Configuration;

public static class DiscountModelBuilder
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<Discount.Domain.Discount>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.EntityId).SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.ObjectId));;
        });
    }
}