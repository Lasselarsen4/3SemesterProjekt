using ModelAPI;
using System.Collections.Generic;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public interface ICartLogic
    {
        Cart GetCartByIndex(int index);
        Cart CreateCart();
        int GetCartIndex(Cart cart);
        Cart AddItemToCart(int index, Product product, int quantity);
        Cart RemoveItemFromCart(int index, int productId);
        List<Cart> GetAllCarts();
    }
}