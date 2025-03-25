using Basket.API.Configuration;
using Basket.API.Extensions;
using Basket.Domain;
using Discount.Domain;
using Microsoft.Extensions.Options;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Basket.API.Gateways;

public class DiscountServiceGateway : IDiscountServiceGateway
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly DiscountServiceEndpoints _discountServiceEndpoints;

    public DiscountServiceGateway(IHttpClientFactory httpClientFactory,
        IOptions<DiscountServiceEndpoints> discountServiceEndpoints)
    {
        _httpClientFactory = httpClientFactory;
        _discountServiceEndpoints = discountServiceEndpoints.Value;
    }

    public async Task<Domain.Basket> RecalculatePrice(Domain.Basket basket, DateTime date)
    {
        var computeDiscountTasks = basket.Items.Select(async item =>
        {
            var url =
                $"{_discountServiceEndpoints.BaseUrl}{_discountServiceEndpoints.GetDiscount}/{item.ProductId}?orderedQuantity={item.Quantity}&productPrice={item.Price}&date={date.AsHttpRequestIsoString()}";

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            try
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var baseDiscount = TryDeserialize<Discount.Domain.Discount>(responseJson);
                
                Discount.Domain.Discount? discount = TryDeserialize<FixedQuantitySalePriceDiscount>(responseJson, baseDiscount.DiscountType, nameof(FixedQuantitySalePriceDiscount));
                discount ??= TryDeserialize<PercentageDiscount>(responseJson, baseDiscount.DiscountType, nameof(PercentageDiscount));
                discount ??= TryDeserialize<SpecialPackDiscount>(responseJson, baseDiscount.DiscountType, nameof(SpecialPackDiscount));

                if (discount is not null)
                {
                    var discountValue = discount.GetDiscountAmount(date, item.Price, item.Quantity);

                    if (discountValue > 0)
                    {
                        return new BasketItem
                        {
                            DiscountAmount = discountValue,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            DiscountDescription = discount.Description,
                        };
                    }
                }
            }
            catch (Exception)
            {
                return item;
            }


            return item;
        });

        var updatedBasketItems = await Task.WhenAll(computeDiscountTasks);

        basket.Items = updatedBasketItems.ToList();

        return basket;
    }

    private T? TryDeserialize<T>(string json, string? actualDiscountType = null, string? expectedDiscountType = null)
    {
        try
        {
            return actualDiscountType != expectedDiscountType ? default : JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception)
        {
            return default;
        }
    }
}