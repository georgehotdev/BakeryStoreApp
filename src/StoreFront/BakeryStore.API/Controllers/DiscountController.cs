using BakeryStore.API.Gateways;
using Catalog.ACL;
using Microsoft.AspNetCore.Mvc;

namespace BakeryStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountServiceGateway _discountServiceGateway;

    public DiscountController(IDiscountServiceGateway discountServiceGateway)
    {
        _discountServiceGateway = discountServiceGateway;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Discount.Domain.Discount>> GetAllDiscounts([FromQuery] DateTime date)
    {
        var products = await _discountServiceGateway.GetAllActiveDiscounts(date);
        return Ok(products);
    }
    
}