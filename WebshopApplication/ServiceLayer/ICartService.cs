using System.Collections.Generic;
using WebshopApplication.Models;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public interface ICartService
    {
        IEnumerable<CartItem> GetCartItems();
        void AddToCart(Product product, int quantity);
        void UpdateCartItem(int productId, int quantity);
        void RemoveFromCart(int productId);
        decimal GetTotalPrice();
        void ClearCart(); // Add this method
    }
}