using BakeryStore.API.Configuration;
using BakeryStore.API.Extensions;
using Microsoft.Extensions.Options;

namespace BakeryStore.API.Gateways;

public class BasketServiceGateway : IBasketServiceGateway
{
    private readonly BasketServiceEndpoints _basketServiceEndpoints;
    private readonly HttpClient _httpClient;

    public BasketServiceGateway(IHttpClientFactory httpClientFactory,
        IOptions<BasketServiceEndpoints> basketServiceEndpoints)
    {
        _basketServiceEndpoints = basketServiceEndpoints.Value;
        _httpClient = httpClientFactory.CreateClient("BasketService");
    }

    public async Task<Basket.Domain.Basket?> UpdateBasket(Basket.Domain.Basket basket, DateTime date)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_basketServiceEndpoints.UpdateBasket}?date={date.AsHttpRequestIsoString()}", basket);
        response.EnsureSuccessStatusCode();
        return await GetBasket(date);
    }

    public async Task<Basket.Domain.Basket?> GetBasket(DateTime date)
    {
        var response = await _httpClient.GetAsync($"{_basketServiceEndpoints.GetBasket}?date={date.AsHttpRequestIsoString()}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Basket.Domain.Basket>();
    }
}