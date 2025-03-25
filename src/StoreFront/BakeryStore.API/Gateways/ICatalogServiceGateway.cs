using Catalog.ACL;

namespace BakeryStore.API.Gateways;

public interface ICatalogServiceGateway
{
    Task<IEnumerable<ProductDto>?> GetAllProducts();
}