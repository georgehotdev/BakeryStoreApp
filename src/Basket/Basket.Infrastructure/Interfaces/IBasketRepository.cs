namespace Basket.Infrastructure.Interfaces;

public interface IBasketRepository
{
    Task<Domain.Basket?> GetBasket(string username);
    Task<Domain.Basket?> UpdateBasket(Domain.Basket basket, string username);
    Task DeleteBasket(string username);

}