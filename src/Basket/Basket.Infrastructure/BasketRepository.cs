using Basket.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
        
    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }
        
    public async Task<Domain.Basket?> GetBasket(string username)
    {
        var basket = await _redisCache.GetStringAsync(username);

        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Domain.Basket>(basket);
    }

    public async Task<Domain.Basket?> UpdateBasket(Domain.Basket basket, string username)
    {
        await _redisCache.SetStringAsync(username, JsonConvert.SerializeObject(basket));

        return await GetBasket(username);
    }

    public async Task DeleteBasket(string username)
    {
        await _redisCache.RemoveAsync(username);
    }
}