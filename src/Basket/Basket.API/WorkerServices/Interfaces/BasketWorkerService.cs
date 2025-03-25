using Basket.API.Gateways;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.WorkerServices.Interfaces;

public class BasketWorkerService : IBasketWorkerService
{
    private readonly IDistributedCache _cache;

    private readonly IDiscountServiceGateway _discountServiceGateway;

    // Initially the service was designed to serve multiple users, but for the purpose of the demo
    // I will hardcode the username
    private const string CurrentUser = "currentUser";

    public BasketWorkerService(IDistributedCache cache, IDiscountServiceGateway discountServiceGateway)
    {
        _cache = cache;
        _discountServiceGateway = discountServiceGateway;
    }
    
    public async Task<Domain.Basket?> GetBasket(DateTime date)
    {
        var basketJson = await _cache.GetStringAsync(CurrentUser);

        if (string.IsNullOrEmpty(basketJson))
        {
            return null;
        }

        var basket =  JsonConvert.DeserializeObject<Domain.Basket>(basketJson);

        if (basket is null)
        {
            return basket;
        }
        
        return await _discountServiceGateway.RecalculatePrice(basket, date);
    }

    public async Task<Domain.Basket?> UpdateBasket(Domain.Basket basket, DateTime date)
    {
        await _cache.SetStringAsync(CurrentUser, JsonConvert.SerializeObject(basket));

        return await GetBasket(date);
    }

    public async Task DeleteBasket()
    {
        await _cache.RemoveAsync(CurrentUser);
    }
}