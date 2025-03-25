using MongoDB.Driver;

namespace Discount.Infrastructure.Interfaces;

public interface IDiscountContext
{
    IMongoCollection<Domain.Discount> Discounts { get; }
}