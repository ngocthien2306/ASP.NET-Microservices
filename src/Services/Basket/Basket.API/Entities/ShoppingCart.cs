namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Itmes { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public decimal TotalPrice 
        { 
            get 
            {
                decimal total = 0;
                foreach(var item in Itmes)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
        }

    }
}
