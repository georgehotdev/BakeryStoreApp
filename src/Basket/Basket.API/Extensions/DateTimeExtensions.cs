namespace Basket.API.Extensions;

public static class DateTimeExtensions
{
    public static string AsHttpRequestIsoString(this DateTime dateTime) => Uri.EscapeDataString(dateTime.ToString("o"));
    
}