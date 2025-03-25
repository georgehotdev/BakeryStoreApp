using BakeryStore.API.Gateways;
using Catalog.ACL;
using Microsoft.AspNetCore.Mvc;

namespace BakeryStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogServiceGateway _catalogServiceGateway;

    public CatalogController(ICatalogServiceGateway catalogServiceGateway)
    {
        _catalogServiceGateway = catalogServiceGateway;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDto>> GetAllProducts()
    {
        var products = await _catalogServiceGateway.GetAllProducts();
        return Ok(products);
    }
}