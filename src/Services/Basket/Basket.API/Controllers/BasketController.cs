using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.IBasketRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcServices _discountGrpcServices;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcServices discountGrpcServices)
        {
            _basketRepository = basketRepository;
            _discountGrpcServices = discountGrpcServices;
        }

        [HttpGet("{username}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(string username)
        {
            var cart = await _basketRepository.GetShoppingCart(username);
            return Ok(cart ?? new ShoppingCart(username));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            // TODO Communication with Discount.Grpc 
            foreach(var item in shoppingCart.Items)
            {
                var coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                item.Price = coupon.Amount;
            }
            return Ok(await _basketRepository.UpdateShoppingCart(shoppingCart));
        }
        [HttpDelete("{username}", Name = "DeleteCart")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteShoppingCart(string username)
        {
            await _basketRepository.DeleteShoppingCart(username);
            return Ok();
        }
    }
}
