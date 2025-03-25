using Catalog.Domain;

namespace Catalog.Infrastructure.Repository.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
}