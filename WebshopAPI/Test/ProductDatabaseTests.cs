using System.Linq;
using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Tests
{
    public class ProductDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllProducts()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); // Mock or use a testing database
            var productDatabase = new ProductDB(dbConnection);

            // Act
            var products = productDatabase.GetAll();

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(1, products.Count()); // Adjust this based on your test data
        }

        // Write similar tests for other methods like GetById, Add, Update, and Delete
    }
}