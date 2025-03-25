using Discount.API.WorkerServices;
using Discount.Infrastructure.Repository.Interfaces;

namespace Discount.Grpc.WorkerServices;

public class DiscountWorkerService : IDiscountWorkerService
{
    private readonly IDiscountRepository _repository;

    public DiscountWorkerService(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<decimal?> GetDiscount(int productId, DateTime date, decimal productPrice, int orderedQuantity)
    {
        var discountBase = await _repository.GetDiscount(productId);
        return discountBase?.GetDiscountAmount(date, productPrice, orderedQuantity);
    }
}