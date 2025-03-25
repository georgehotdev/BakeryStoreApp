namespace Discount.Domain;

public class PercentageDiscount : Discount
{
    public PercentageDiscount()
    {
        DiscountType = nameof(PercentageDiscount);
    }

    public int DiscountPercentage { get; set; }

    public override decimal GetDiscountAmount(DateTime date, decimal productPrice, int orderedQuantity)
    {
        if (!IsDiscountValidForDate(date))
        {
            return 0m;
        }

        return DiscountPercentage * productPrice * orderedQuantity / 100.0m;
    }
}