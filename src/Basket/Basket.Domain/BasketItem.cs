namespace Basket.Domain;

public class BasketItem
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? DiscountDescription { get; set; }
    public decimal? DiscountAmount { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}