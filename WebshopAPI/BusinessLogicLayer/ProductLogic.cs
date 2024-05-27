using ModelAPI;
using WebshopAPI.Database;

namespace WebshopAPI.BusinessLogicLayer
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductDB _productDB;

        public ProductLogic(IProductDB productDB)
        {
            _productDB = productDB;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productDB.GetAll();
        }

        public Product GetProductById(int productId)
        {
            return _productDB.GetById(productId);
        }

        public void AddProduct(Product product)
        {
            _productDB.Add(product);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            _productDB.Update(updatedProduct);
        }

        public void DeleteProduct(int productId)
        {
            _productDB.Delete(productId);
        }
    }
}