using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Tests
{
    public class OrderLineDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllOrderLines()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); // Mock or use a testing database
            var orderLineDatabase = new OrderLineDB(dbConnection);

            // Act
            var orderLines = orderLineDatabase.GetAll();

            // Assert
            Assert.NotNull(orderLines);
            Assert.NotEmpty(orderLines);
            // Adjust the count based on the number of order lines in your test data
            Assert.Equal(1, orderLines.Count());
        }

        // Add more test methods as needed to test other functionalities of OrderLineDB
    }
}