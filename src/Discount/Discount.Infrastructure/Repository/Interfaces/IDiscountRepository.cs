namespace Discount.Infrastructure.Repository.Interfaces;

public interface IDiscountRepository
{
    Task<Domain.Discount?> GetDiscount(int productId);
    Task<IEnumerable<Domain.Discount>> GetAllActiveDiscounts(DateTime date);
}