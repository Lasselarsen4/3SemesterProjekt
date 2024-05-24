using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopApplication.Models;

namespace DesktopApplication.ServiceLayer;

public interface IProductService
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProductById(int productId);
    Task<bool> SaveProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(int productId);
}