namespace Discount.Infrastructure.Settings;

public interface IDiscountDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }
}