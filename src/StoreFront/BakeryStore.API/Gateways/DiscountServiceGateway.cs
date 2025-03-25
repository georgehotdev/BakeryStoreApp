using BakeryStore.API.Configuration;
using BakeryStore.API.Extensions;
using Catalog.ACL;
using Microsoft.Extensions.Options;

namespace BakeryStore.API.Gateways;

public class DiscountServiceGateway : IDiscountServiceGateway
{
    private readonly DiscountServiceEndpoints _catalogServiceEndpoints;
    private readonly HttpClient _httpClient;

    public DiscountServiceGateway(IHttpClientFactory httpClientFactory, IOptions<DiscountServiceEndpoints> catalogServiceEndpoints)
    {
        _catalogServiceEndpoints = catalogServiceEndpoints.Value;
        _httpClient = httpClientFactory.CreateClient("DiscountService");
    }
    
    public async Task<IEnumerable<Discount.Domain.Discount>?> GetAllActiveDiscounts(DateTime date)
    {
        var url = $"{_catalogServiceEndpoints.GetAllActiveDiscounts}?date={date.AsHttpRequestIsoString()}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Discount.Domain.Discount>>();
    }
}