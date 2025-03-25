using Cronos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Discount.Domain;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(PercentageDiscount))]
[BsonKnownTypes(typeof(FixedQuantitySalePriceDiscount))]
[BsonKnownTypes(typeof(SpecialPackDiscount))]
public class Discount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string EntityId { get; set; }
    public int ProductId { get; set; }
    public string Description { get; set; }
    private string _recurrenceCron { get; set; }
    
    public string DiscountType { get; set; }

    public string RecurrenceCron
    {
        get => _recurrenceCron;
        set
        {
            if (!CronExpression.TryParse(value, out _))
            {
                throw new ArgumentException("Invalid Cron expression");
            }

            _recurrenceCron = value;
        }
    }

    public bool IsDiscountValidForDate(DateTime date)
    {
        var cronExpression = CronExpression.Parse(_recurrenceCron);
        var nextOccurrence = cronExpression.GetNextOccurrence(date.ToUniversalTime().AddDays(-1));
        var isValidForDiscount = nextOccurrence.HasValue && nextOccurrence.Value.Date == date.Date;

        return isValidForDiscount;
    }

    public virtual decimal GetDiscountAmount(DateTime date, decimal productPrice, int orderedQuantity)
    {
        return 0.0m;
    }
}