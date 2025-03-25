namespace Discount.Domain;

public class FixedQuantitySalePriceDiscount : Discount
{
    public FixedQuantitySalePriceDiscount()
    {
        DiscountType = nameof(FixedQuantitySalePriceDiscount);
    }
    
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }


    public override decimal GetDiscountAmount(DateTime date, decimal productPrice, int orderedQuantity)
    {
        if (!IsDiscountValidForDate(date))
        {
            return 0;
        }

        if (orderedQuantity < Quantity)
        {
            return 0;
        }

        var priceBeforeDiscount = productPrice * orderedQuantity;
        var quantityMultiplier = orderedQuantity / Quantity;
        var remainingQuantity = orderedQuantity % Quantity;
        var priceAfterDiscount = quantityMultiplier * SalePrice + remainingQuantity * productPrice;

        return priceBeforeDiscount - priceAfterDiscount;
    }
}