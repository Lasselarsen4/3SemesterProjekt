using ModelAPI;
using System.Threading.Tasks;

namespace WebshopApplication.ServiceLayer
{
    public interface IProductService
    {
        Task<Product> GetById(int productId);
        Task<List<Product>> GetProducts(string sortParam, int id = -1);
        Task<bool> SaveProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}