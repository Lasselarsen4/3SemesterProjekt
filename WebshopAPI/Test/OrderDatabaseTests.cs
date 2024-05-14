using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Test
{
    public class OrderDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllOrders()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); 
            var orderDatabase = new OrderDB(dbConnection);

            // Act
            var orders = orderDatabase.GetAll();

            // Assert
            Assert.NotNull(orders);
            Assert.NotEmpty(orders);
            Assert.Equal(1, orders.Count());


        }
        [Fact]
        public void GetById_ReturnsOrderWithValidId()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();
            var orderDatabase = new OrderDB(dbConnection);
            var orderId = 1; 

            // Act
            var order = orderDatabase.GetById(orderId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(orderId, order.OrderId);
        }
        [Fact]
        public void GetById_ReturnsNullWithInvalidId()
        {
            // Arrange
            var dbConnection = new DatabaseConnection(); 
            var orderDatabase = new OrderDB(dbConnection);
            var invalidOrderId = -1; 

            // Act
            var order = orderDatabase.GetById(invalidOrderId);

            // Assert
            Assert.Null(order);
        }
    }
}
