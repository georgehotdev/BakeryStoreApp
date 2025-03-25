using Catalog.ACL;
using Catalog.Domain;

namespace Catalog.API.Mappers;

public static class ProductMapper
{
    public static ProductDto ToProductViewModel(this Product product)
    {
        return new ProductDto
        {
            ImageUrl = product.ImageUrl,
            Name = product.Name,
            Price = product.Price,
            Id = product.Id,
        };
    }
}