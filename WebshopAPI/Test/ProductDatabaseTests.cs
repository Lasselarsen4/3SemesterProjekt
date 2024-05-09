using NUnit.Framework;
using WebshopAPI.Database;
using Model;
using System.Linq;

namespace WebshopAPI.Tests
{
    [TestFixture]
    public class ProductDatabaseTests
    {
        private DatabaseConnection _dbConnection;
        private ProductDatabase _productDatabase;

        [SetUp]
        public void SetUp()
        {
            _dbConnection = new DatabaseConnection(); // Initialize with appropriate connection details
            _productDatabase = new ProductDatabase(_dbConnection);
        }

        [Test]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange: Add test data to the database
            var expectedProducts = new Product[]
            {
                new Product { ProductName = "TestProduct1", ProductPrice = 10.00m, ProductDescription = "Test Description 1" },
                new Product { ProductName = "TestProduct2", ProductPrice = 20.00m, ProductDescription = "Test Description 2" }
            };
            foreach (var product in expectedProducts)
            {
                _productDatabase.AddProduct(product);
            }

            // Act: Retrieve all products
            var actualProducts = _productDatabase.GetAllProducts();

            // Assert: Check if all expected products are returned
            Assert.AreEqual(expectedProducts.Length, actualProducts.Count());
            foreach (var expectedProduct in expectedProducts)
            {
                Assert.IsTrue(actualProducts.Any(p => p.ProductName == expectedProduct.ProductName && p.ProductPrice == expectedProduct.ProductPrice && p.ProductDescription == expectedProduct.ProductDescription));
            }

            // Clean up: Remove test data from the database
            foreach (var product in expectedProducts)
            {
                _productDatabase.DeleteProduct(product.ProductId);
            }
        }

        // Similar methods for testing other CRUD operations...
    }
}