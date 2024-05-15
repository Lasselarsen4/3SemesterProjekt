using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelAPI;
using WebshopApplication.ServiceLayer;

namespace WebshopApplication.BusinessLogicLayerWeb
{
    public class ProductLogic
    {
        private readonly ProductService _productService;

        public ProductLogic(IConfiguration inConfiguration)
        {
            _productService = new ProductService(inConfiguration);
        }

        public async Task<List<Product>?> GetProducts(string? sortParam, int id = -1)
        {
            try
            {
                return await _productService.GetProducts(sortParam, id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var products = await _productService.GetProducts(null, id);
                return products?.Count > 0 ? products[0] : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> InsertProduct(Product product)
        {
            try
            {
                return await _productService.SaveProduct(product);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                return await _productService.UpdateProduct(product);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int delId)
        {
            try
            {
                return await _productService.DeleteProduct(delId);
            }
            catch
            {
                return false;
            }
        }
    }
}