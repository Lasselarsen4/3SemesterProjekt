using System.Collections.Generic;
using System.Threading.Tasks;
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(string sortParam, int id = -1);
        Task<bool> SaveProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}