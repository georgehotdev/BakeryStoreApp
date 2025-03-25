using BakeryStore.API.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace BakeryStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketServiceGateway _basketServiceGateway;

    public BasketController(IBasketServiceGateway basketServiceGateway)
    {
        _basketServiceGateway = basketServiceGateway;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Basket.Domain.Basket), StatusCodes.Status200OK)]
    public async Task<ActionResult<Basket.Domain.Basket>> GetBasket(DateTime date)
    {
        return Ok(await _basketServiceGateway.GetBasket(date));
    }

    [HttpPut]
    [ProducesResponseType(typeof(Basket.Domain.Basket), StatusCodes.Status200OK)]
    public async Task<ActionResult<Basket.Domain.Basket>> UpdateBasket([FromBody] Basket.Domain.Basket basket, [FromQuery]DateTime date)
    {
        return Ok(await _basketServiceGateway.UpdateBasket(basket, date));
    }
}