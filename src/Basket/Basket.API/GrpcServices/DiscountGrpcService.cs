﻿using System;
using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<DiscountModel> GetDiscount(int productId)
        {
            return await _discountProtoService.GetDiscountAsync(new GetDiscountRequest
            {
                ProductId = productId
            });
        }
    }
}
