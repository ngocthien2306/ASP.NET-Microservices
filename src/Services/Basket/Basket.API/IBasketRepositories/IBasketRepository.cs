using Basket.API.Entities;

namespace Basket.API.IBasketRepositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCart(string username);
        Task<ShoppingCart> UpdateShoppingCart(ShoppingCart cart);
        Task DeleteShoppingCart(string username);
    }
}
