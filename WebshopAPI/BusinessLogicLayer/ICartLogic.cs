using System.Collections.Generic;
using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface ICartLogic
    {
        IEnumerable<Cart> GetAllCarts();
        Cart GetCartById(int id);
        void AddCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(int id);
    }
}