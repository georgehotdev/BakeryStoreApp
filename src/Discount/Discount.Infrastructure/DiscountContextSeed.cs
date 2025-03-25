using Discount.Domain;
using MongoDB.Driver;

namespace Discount.Infrastructure;

public class DiscountContextSeed
{
    public static void SeedData(IMongoCollection<Discount.Domain.Discount> discountCollection)
    {
        var productsAlreadySeeded = discountCollection.Find(p => true).Any();
        if (!productsAlreadySeeded)
        {
            discountCollection.InsertMany(GetSeedData());
        }
    }

    private static IEnumerable<Discount.Domain.Discount> GetSeedData()
    {
        return new List<Discount.Domain.Discount>
        {
            new FixedQuantitySalePriceDiscount()
            {
                ProductId = 3,
                Description = "8 Cookies $6.00",
                Quantity = 8,
                SalePrice = 6,
                RecurrenceCron = "0 0 * * 5",
            },
            new PercentageDiscount()
            {
                ProductId = 2,
                Description = "25% off",
                DiscountPercentage = 25,
                RecurrenceCron = "0 0 1 10 *",
            },
            new SpecialPackDiscount()
            {
                ProductId = 4,
                Description = "Two for One",
                PricingQuantity = 1,
                DeliveringQuantity = 2,
                RecurrenceCron = "0 0 * * 2",
            },
        };
    }
}