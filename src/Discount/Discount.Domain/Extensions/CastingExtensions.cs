namespace Discount.Domain.Extensions;

public static class CastingExtensions
{
    public static T? CastToSubtype<T>(this Discount discount, string subtypeName) where T : class
    {
        var subtype = Type.GetType(subtypeName);
        if (subtype != null)
        {
            return Convert.ChangeType(discount, subtype) as T;
        }
        return null;
    }

    public static Discount? CastToSubType(this Discount? discount)
    {
        if (discount is null)
        {
            return null;
        }
        
        switch (discount.DiscountType)
        {
            case nameof(FixedQuantitySalePriceDiscount) : return discount.CastToSubtype<FixedQuantitySalePriceDiscount>($"Discount.Domain.{nameof(FixedQuantitySalePriceDiscount)}");
            case nameof(PercentageDiscount) : return discount.CastToSubtype<PercentageDiscount>($"Discount.Domain.{nameof(PercentageDiscount)}");
            case nameof(SpecialPackDiscount) : return discount.CastToSubtype<SpecialPackDiscount>($"Discount.Domain.{nameof(SpecialPackDiscount)}");
            default: return discount;
        }
    }
}