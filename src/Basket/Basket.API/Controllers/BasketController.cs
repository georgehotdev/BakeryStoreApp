using System;
using System.Net;
using System.Threading.Tasks;
using Basket.API.WorkerServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketWorkerService _workerService;

    public BasketController(IBasketWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet(Name = "GetBasket")]
    [ProducesResponseType(typeof(Domain.Basket), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Domain.Basket>> GetBasket(DateTime date)
    {
        var basket = await _workerService.GetBasket(date);
        return Ok(basket ?? new Domain.Basket());
    }

    [HttpPut]
    [ProducesResponseType(typeof(Domain.Basket), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Domain.Basket>> UpdateBasket([FromBody] Domain.Basket basket,
        [FromQuery] DateTime date)
    {
        var result = await _workerService.UpdateBasket(basket, date);
        return Ok(result);
    }

    [HttpDelete(Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket()
    {
        await _workerService.DeleteBasket();

        return Ok();
    }
}