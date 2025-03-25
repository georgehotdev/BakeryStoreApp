using Catalog.ACL;

namespace Catalog.API.WorkerServices.Interfaces;

public interface ICatalogWorkerService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}