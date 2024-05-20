using System.Collections.Generic;
using ModelAPI;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class CartLogic : ICartLogic
    {
        private readonly ICartDB _cartDb;

        public CartLogic(ICartDB cartDb)
        {
            _cartDb = cartDb;
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _cartDb.GetAll();
        }

        public Cart GetCartById(int id)
        {
            return _cartDb.GetById(id);
        }

        public void AddCart(Cart cart)
        {
            _cartDb.Add(cart);
        }

        public void UpdateCart(Cart cart)
        {
            _cartDb.Update(cart);
        }

        public void DeleteCart(int id)
        {
            _cartDb.Delete(id);
        }
    }
}