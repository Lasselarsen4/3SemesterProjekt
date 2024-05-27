using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICartLogic
    {
        Cart GetCart();
        void AddToCart(Product product, int quantity);
        void UpdateCartItem(int productId, int quantity);
        void RemoveFromCart(int productId);
        decimal GetTotalPrice();
    }
}