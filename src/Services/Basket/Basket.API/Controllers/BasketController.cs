using Basket.API.Entities;
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
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
