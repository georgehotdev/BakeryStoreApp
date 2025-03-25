using Discount.Domain;
using Discount.Domain.Extensions;
using Discount.Infrastructure.Repository.Interfaces;

namespace Discount.API.WorkerServices;

public class DiscountWorkerService : IDiscountWorkerService
{
    private readonly IDiscountRepository _repository;

    public DiscountWorkerService(IDiscountRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Domain.Discount?> GetDiscount(int productId, DateTime date, decimal productPrice,
        int orderedQuantity)
    {
        var discount = await _repository.GetDiscount(productId);

        if (discount is null)
        {
            return discount;
        }

        return discount.CastToSubType();
    }

    public async Task<IEnumerable<Domain.Discount>?> GetAllActiveDiscounts(DateTime date)
    {
        return await _repository.GetAllActiveDiscounts(date);
    }
}