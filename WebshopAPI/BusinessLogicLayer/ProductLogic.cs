using ModelAPI;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.BusinessLogicLayer
{
    public class ProductLogic : IProductLogic
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }

        public void AddProduct(Product product)
        {
            product.ProductId = _products.Count + 1;
            _products.Add(product);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            var productToUpdate = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (productToUpdate != null)
            {
                productToUpdate.ProductName = updatedProduct.ProductName;
                productToUpdate.ProductPrice = updatedProduct.ProductPrice;
                productToUpdate.ProductDescription = updatedProduct.ProductDescription;
            }
        }

        public void DeleteProduct(int productId)
        {
            var productToRemove = _products.FirstOrDefault(p => p.ProductId == productId);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }
    }
}