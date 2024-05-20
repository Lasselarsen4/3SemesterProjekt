using ModelAPI;
using System.Collections.Generic;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CartLogic : ICartLogic
    {
        private static readonly Dictionary<string, Cart> UserCarts = new Dictionary<string, Cart>();
        private readonly IProductDB _productDB;

        public CartLogic(IProductDB productDB)
        {
            _productDB = productDB;
        }

        public Cart GetCartByUser(string userId)
        {
            UserCarts.TryGetValue(userId, out var cart);
            return cart;
        }

        public Cart CreateCart(string userId)
        {
            var newCart = new Cart();
            UserCarts[userId] = newCart;
            return newCart;
        }

        public Cart AddItemToCart(string userId, Product product, int quantity = 1)
        {
            if (!UserCarts.TryGetValue(userId, out var cart))
            {
                cart = CreateCart(userId);
            }

            cart.AddItem(product, quantity);
            return cart;
        }

        public Cart RemoveItemFromCart(string userId, int productId)
        {
            if (UserCarts.TryGetValue(userId, out var cart))
            {
                cart.RemoveItem(productId);
                return cart;
            }
            return null;
        }
    }
}