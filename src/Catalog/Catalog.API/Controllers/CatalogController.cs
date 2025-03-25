using System.Net;
using Catalog.ACL;
using Catalog.API.WorkerServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogWorkerService _workerService;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ICatalogWorkerService workerService, ILogger<CatalogController> logger)
    {
        _workerService = workerService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _workerService.GetProducts();
        return Ok(products);
    }
}