namespace Basket.API.Gateways;

public interface IDiscountServiceGateway
{
    Task<Domain.Basket> RecalculatePrice(Domain.Basket basket, DateTime date);
}