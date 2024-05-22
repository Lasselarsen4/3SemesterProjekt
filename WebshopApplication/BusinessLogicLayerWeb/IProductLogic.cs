using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public interface IProductLogic
    {
        Task<List<Product>> GetProducts(string sortParam);
        Task<Product> GetProductById(int id);
        Task<bool> InsertProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}