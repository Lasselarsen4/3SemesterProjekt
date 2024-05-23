using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopApplication.Models;

namespace WebshopApplication.ServiceLayer
{
    public interface IProductService
    {
        Task<Product> GetById(int productId);
        Task<List<Product>> GetProducts(string sortParam, int id = -1);
        Task<bool> SaveProduct(Product product);
        Task<bool> UpdateProduct(Product product);  // Ensure this method exists
        Task<bool> DeleteProduct(int id);
    }
}