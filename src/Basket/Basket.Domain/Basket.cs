namespace Basket.Domain;

public class Basket
{
    public List<BasketItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

}