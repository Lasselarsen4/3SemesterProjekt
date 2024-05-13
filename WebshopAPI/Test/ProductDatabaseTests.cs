using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Test
{
    public class ProductDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllProducts()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); 
            var productDatabase = new ProductDB(dbConnection);

            // Act
            var products = productDatabase.GetAll();

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(1, products.Count());
        }
        
    }
}