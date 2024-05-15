using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CartLogic : ICartLogic
    {
        private static readonly Dictionary<int, Cart> _carts = new Dictionary<int, Cart>();

        public Cart GetCartById(int cartId)
        {
            if (_carts.TryGetValue(cartId, out var cart))
            {
                return cart;
            }
            else
            {
                return null;
            }
        }

        public Cart AddItemToCart(int cartId, Product product, int quantity = 1)
        {
            if (_carts.TryGetValue(cartId, out var cart))
            {
                cart.AddItem(product, quantity);
                return cart;
            }
            else
            {
                var newCart = new Cart();
                newCart.AddItem(product, quantity);
                _carts.Add(newCart.CartId, newCart);
                return newCart;
            }
        }

        public Cart RemoveItemFromCart(int cartId, int productId)
        {
            if (_carts.TryGetValue(cartId, out var cart))
            {
                cart.RemoveItem(productId);
                return cart;
            }
            else
            {
                return null;
            }
        }
    }
}