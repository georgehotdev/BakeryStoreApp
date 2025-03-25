using Catalog.Domain;
using Catalog.Infrastructure.Interfaces;
using Catalog.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.Find(p => true).ToListAsync();
    }
}