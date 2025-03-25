using Discount.Infrastructure.Interfaces;
using Discount.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;

namespace Discount.Infrastructure.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly IDiscountContext _discountContext;

    public DiscountRepository(IDiscountContext discountContext)
    {
        _discountContext = discountContext;
    }
    
    public async Task<Domain.Discount?> GetDiscount(int productId)
    {
        return await _discountContext.Discounts.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Domain.Discount>> GetAllActiveDiscounts(DateTime date)
    {
        var allDiscounts = await _discountContext.Discounts.Find(p => true).ToListAsync() ?? new List<Domain.Discount>();

        return allDiscounts.Where(d => d.IsDiscountValidForDate(date));
    }
}