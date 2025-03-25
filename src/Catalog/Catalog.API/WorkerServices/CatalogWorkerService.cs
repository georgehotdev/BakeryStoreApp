using Catalog.ACL;
using Catalog.API.Mappers;
using Catalog.API.WorkerServices.Interfaces;
using Catalog.Infrastructure.Repository.Interfaces;

namespace Catalog.API.WorkerServices;

public class CatalogWorkerService : ICatalogWorkerService
{
    private readonly IProductRepository _repository;

    public CatalogWorkerService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await _repository.GetProducts();
        return products.Select(p => p.ToProductViewModel());
    }
}