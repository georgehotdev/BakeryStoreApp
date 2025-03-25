namespace Discount.API.WorkerServices;

public interface IDiscountWorkerService
{
    Task<Domain.Discount?> GetDiscount(int productId, DateTime date, decimal productPrice, int orderedQuantity);
    Task<IEnumerable<Domain.Discount>?> GetAllActiveDiscounts(DateTime date);
}