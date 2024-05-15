using ModelAPI;
using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IProductDB
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}