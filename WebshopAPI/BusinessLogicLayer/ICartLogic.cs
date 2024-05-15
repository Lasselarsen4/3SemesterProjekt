using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICartLogic
    {
        Cart GetCartById(int cartId);
        Cart AddItemToCart(int cartId, Product product, int quantity = 1);
        Cart RemoveItemFromCart(int cartId, int productId);
    }
}