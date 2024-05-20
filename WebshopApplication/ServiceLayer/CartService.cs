using System.Collections.Generic;
using System.Linq;
using WebshopApplication.Models;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public class CartService : ICartService
    {
        private static Cart _cart = new Cart();

        public IEnumerable<CartItem> GetCartItems()
        {
            return _cart.Items;
        }

        public void AddToCart(Product product, int quantity)
        {
            _cart.AddItem(product, quantity);
        }

        public void UpdateCartItem(int productId, int quantity)
        {
            _cart.UpdateItem(productId, quantity);
        }

        public void RemoveFromCart(int productId)
        {
            _cart.RemoveItem(productId);
        }

        public decimal GetTotalPrice()
        {
            return _cart.GetTotalPrice();
        }
    }
}