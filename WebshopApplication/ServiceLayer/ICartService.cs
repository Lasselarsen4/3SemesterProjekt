using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface ICartService
    {
        IEnumerable<CartItem> GetCartItems();
        void AddToCart(Product product, int quantity);
        void UpdateCartItem(int productId, int quantity);
        void RemoveFromCart(int productId);
        decimal GetTotalPrice();
        void ClearCart();
    }
}