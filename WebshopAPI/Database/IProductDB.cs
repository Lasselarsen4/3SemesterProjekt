using System.Collections.Generic;

namespace WebshopAPI.Database
{
    public interface IProductDB<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}