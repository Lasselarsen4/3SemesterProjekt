using ModelAPI;

namespace WebshopAPI.BusinessLogicLayer
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product updatedProduct);
        void DeleteProduct(int productId);
    }
}