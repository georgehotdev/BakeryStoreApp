using Basket.Domain;

namespace BakeryStore.API.Gateways;

public interface IBasketServiceGateway
{
    Task<Basket.Domain.Basket?> UpdateBasket(Basket.Domain.Basket basket, DateTime date);
    Task<Basket.Domain.Basket?> GetBasket(DateTime date);
}