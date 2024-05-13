using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Test
{
    public class OrderLineDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllOrderLines()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();
            var orderLineDatabase = new OrderLineDB(dbConnection);

            // Act
            var orderLines = orderLineDatabase.GetAll();

            // Assert
            Assert.NotNull(orderLines);
            Assert.NotEmpty(orderLines);
            Assert.Equal(1, orderLines.Count());
        }

        
    }
}