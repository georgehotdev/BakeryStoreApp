namespace Basket.API.WorkerServices.Interfaces;

public interface IBasketWorkerService
{
    Task<Domain.Basket?> GetBasket(DateTime date);
    Task<Domain.Basket?> UpdateBasket(Domain.Basket basket, DateTime date);
    Task DeleteBasket();
}