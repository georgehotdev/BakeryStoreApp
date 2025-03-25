using BakeryStore.API.Configuration;
using Catalog.ACL;
using Microsoft.Extensions.Options;

namespace BakeryStore.API.Gateways;

public class CatalogServiceGateway : ICatalogServiceGateway
{
    private readonly CatalogServiceEndpoints _catalogServiceEndpoints;
    private readonly HttpClient _httpClient;

    public CatalogServiceGateway(IHttpClientFactory httpClientFactory, IOptions<CatalogServiceEndpoints> catalogServiceEndpoints)
    {
        _catalogServiceEndpoints = catalogServiceEndpoints.Value;
        _httpClient = httpClientFactory.CreateClient("CatalogService");
    }
    
    public async Task<IEnumerable<ProductDto>?> GetAllProducts()
    {
        var response = await _httpClient.GetAsync(_catalogServiceEndpoints.GetAllProducts);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
    }
}