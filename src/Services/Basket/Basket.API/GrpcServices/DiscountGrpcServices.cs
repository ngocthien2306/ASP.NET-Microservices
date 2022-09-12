using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcServices
    {
        private readonly DiscountProtoServices.DiscountProtoServicesClient _protoServicesBase;

        public DiscountGrpcServices(DiscountProtoServices.DiscountProtoServicesClient protoServicesBase)
        {
            _protoServicesBase = protoServicesBase;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _protoServicesBase.GetDiscountAsync(discountRequest);
        }
    }
}
