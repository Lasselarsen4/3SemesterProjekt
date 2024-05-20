using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICartLogic
    {
        Cart GetCartByUser(string userId);
        Cart CreateCart(string userId);
        Cart AddItemToCart(string userId, Product product, int quantity);
        Cart RemoveItemFromCart(string userId, int productId);
    }
}