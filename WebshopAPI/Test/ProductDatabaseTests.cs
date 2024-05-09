using System;
using System.Collections.Generic;
using System.Linq;
using WebshopAPI.Database;
using Model;

namespace WebshopAPI.Test
{
    public class ProductDatabaseTests
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly ProductDatabase _productDatabase;

        public ProductDatabaseTests()
        {
            _dbConnection = new DatabaseConnection(); // Initialize with appropriate connection details
            _productDatabase = new ProductDatabase(_dbConnection);
        }

        public void TestGetAllProducts()
        {
            // Arrange: Add test data to the database
            var expectedProducts = new List<Product>
            {
                new Product { ProductName = "TestProduct1", ProductPrice = 10.00m, ProductDescription = "Test Description 1" },
                new Product { ProductName = "TestProduct2", ProductPrice = 20.00m, ProductDescription = "Test Description 2" }
            };
            expectedProducts.ForEach(product => _productDatabase.AddProduct(product));

            // Act: Retrieve all products
            var actualProducts = _productDatabase.GetAllProducts();

            // Assert: Check if all expected products are returned
            if (expectedProducts.Count != actualProducts.Count())
            {
                Console.WriteLine("GetAllProducts test failed: Incorrect number of products returned.");
                return;
            }

            foreach (var expectedProduct in expectedProducts)
            {
                var actualProduct = actualProducts.FirstOrDefault(p =>
                    p.ProductName == expectedProduct.ProductName &&
                    p.ProductPrice == expectedProduct.ProductPrice &&
                    p.ProductDescription == expectedProduct.ProductDescription);

                if (actualProduct == null)
                {
                    Console.WriteLine($"GetAllProducts test failed: Product '{expectedProduct.ProductName}' not found.");
                    return;
                }
            }

            Console.WriteLine("GetAllProducts test passed.");
        }

        // Add more test methods for other CRUD operations...
    }

}
