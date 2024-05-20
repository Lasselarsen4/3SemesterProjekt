using System.Collections.Generic;
using ModelAPI;

namespace WebshopAPI.Database
{
    public interface ICartDB
    {
        IEnumerable<Cart> GetAll();
        Cart GetById(int id);
        void Add(Cart cart);
        void Update(Cart cart);
        void Delete(int id);
    }
}