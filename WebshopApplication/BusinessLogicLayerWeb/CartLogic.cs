using ModelAPI;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class CartLogic : ICartLogic
    {
        private static readonly List<Cart> Carts = new List<Cart>();

        public Cart GetCartByIndex(int index)
        {
            return index >= 0 && index < Carts.Count ? Carts[index] : null;
        }

        public Cart CreateCart()
        {
            var newCart = new Cart();
            Carts.Add(newCart);
            return newCart;
        }

        public int GetCartIndex(Cart cart)
        {
            return Carts.IndexOf(cart);
        }

        public Cart AddItemToCart(int index, Product product, int quantity)
        {
            var cart = GetCartByIndex(index);
            if (cart != null)
            {
                cart.AddItem(product, quantity);
                return cart;
            }
            else
            {
                var newCart = CreateCart();
                newCart.AddItem(product, quantity);
                return newCart;
            }
        }

        public Cart RemoveItemFromCart(int index, int productId)
        {
            var cart = GetCartByIndex(index);
            if (cart != null)
            {
                cart.RemoveItem(productId);
                return cart;
            }
            return null;
        }

        public List<Cart> GetAllCarts()
        {
            return Carts;
        }
    }
}