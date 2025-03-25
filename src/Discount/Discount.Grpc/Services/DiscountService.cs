using Discount.Grpc.Protos;
using Discount.Infrastructure.Repository.Interfaces;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;

    public DiscountService(IDiscountRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public override async Task<DiscountModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var discount = await _repository.GetDiscount(request.ProductId);

        if (discount == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with ProductId={request.ProductId} could not be found"));
        }

        return new DiscountModel
        {
            ProductId = discount.ProductId,
            Amount = (double)discount.GetDiscountAmount(request.Date.ToDateTime(), (decimal)request.ProductPrice,
                request.OrderedQuantity),
            Description = discount.Description,
            Id = discount.EntityId
        };
    }
}