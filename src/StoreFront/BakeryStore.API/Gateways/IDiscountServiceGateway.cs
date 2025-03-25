namespace BakeryStore.API.Gateways;

public interface IDiscountServiceGateway
{
    Task<IEnumerable<Discount.Domain.Discount>?> GetAllActiveDiscounts(DateTime date);
}