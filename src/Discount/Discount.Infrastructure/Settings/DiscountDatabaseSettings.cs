namespace Discount.Infrastructure.Settings;

public class DiscountDatabaseSettings : IDiscountDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}