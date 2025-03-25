using System.Net;
using Discount.API.WorkerServices;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountWorkerService _workerService;

    public DiscountController(IDiscountWorkerService workerService)
    {
        _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
    }
    
    [HttpGet("active", Name = "GetAllActiveDiscounts")]
    [ProducesResponseType(typeof(IEnumerable<Domain.Discount>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Domain.Discount>>> GetAllActiveDiscounts(DateTime date)
    {
        if (date == DateTime.MinValue)
        {
            return Ok(new List<Domain.Discount>());
        }
        var discount = await _workerService.GetAllActiveDiscounts(date);

        return Ok(discount);
    }

    [HttpGet("product/{productId}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Domain.Discount), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Domain.Discount>> GetDiscount(int productId, DateTime date, decimal productPrice,
        int orderedQuantity)
    {
        var discount = await _workerService.GetDiscount(productId, date, productPrice, orderedQuantity);

        return Ok(discount);
    }
}