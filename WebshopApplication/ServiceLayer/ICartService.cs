using System.Threading.Tasks;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public interface ICartService
    {
        Task<Cart> GetCartByUser(string userId);
        Task<Cart> CreateCart(string userId);
        Task<Cart> AddItemToCart(string userId, Product product, int quantity);
        Task<Cart> RemoveItemFromCart(string userId, int productId);
    }
}