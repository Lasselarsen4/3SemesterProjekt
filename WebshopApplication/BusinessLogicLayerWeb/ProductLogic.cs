using WebshopApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductService _productService;

        public ProductLogic(IConfiguration configuration)
        {
            _productService = new ProductService(configuration);
        }

        public Task<List<Product>> GetProducts(string sortParam)
        {
            return _productService.GetProducts(sortParam);
        }

        public Task<Product> GetProductById(int id)
        {
            return _productService.GetProducts(null, id)
                .ContinueWith(task => task.Result != null && task.Result.Count > 0 ? task.Result[0] : null);
        }

        public Task<bool> InsertProduct(Product product)
        {
            return _productService.SaveProduct(product);
        }

        public Task<bool> UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        public Task<bool> DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }

        public async Task<bool> UpdateProductStock(int productId, int quantity, byte[] rowVersion)
        {
            var product = await GetProductById(productId);
            if (product == null)
            {
                return false;
            }

            product.Stock -= quantity;
            product.RowVersion = rowVersion; // Set the RowVersion for concurrency check
            return await UpdateProduct(product);
        }
    }
}