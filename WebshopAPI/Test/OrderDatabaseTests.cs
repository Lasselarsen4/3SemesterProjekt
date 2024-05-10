using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Tests
{
    public class OrderDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllOrders()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); // Mock or use a testing database
            var orderDatabase = new OrderDB(dbConnection);

            // Act
            var orders = orderDatabase.GetAll();

            // Assert
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
            // Adjust the count based on the number of orders in your test data
            Assert.Equal(1, orders.Count());


        }

        // BASICALLY TESTER VI PÅ OM DER OVERHOVEDET KAN HENTES EN ORDER FRA DATABASEN
       
    }
}
