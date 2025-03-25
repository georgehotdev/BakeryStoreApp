namespace Discount.Domain;

public class SpecialPackDiscount : Discount
{
    public SpecialPackDiscount()
    {
        DiscountType = nameof(SpecialPackDiscount);
    }
    
    public int DeliveringQuantity { get; set; }
    public int PricingQuantity { get; set; }
    
    public override decimal GetDiscountAmount(DateTime date, decimal productPrice, int orderedQuantity)
    {
        if (!IsDiscountValidForDate(date))
        {
            return 0;
        }       
        
        var priceBeforeDiscount = productPrice * orderedQuantity;
        var quantityMultiplier = orderedQuantity / DeliveringQuantity;
        var remainingQuantity = orderedQuantity % DeliveringQuantity;
        var priceAfterDiscount = quantityMultiplier * PricingQuantity * productPrice + remainingQuantity * productPrice;

        return priceBeforeDiscount - priceAfterDiscount;
    }
}