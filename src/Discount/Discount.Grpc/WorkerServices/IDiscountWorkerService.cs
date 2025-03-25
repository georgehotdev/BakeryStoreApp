namespace Discount.API.WorkerServices;

public interface IDiscountWorkerService
{
    Task<decimal?> GetDiscount(int productId, DateTime date, decimal productPrice, int orderedQuantity);
}