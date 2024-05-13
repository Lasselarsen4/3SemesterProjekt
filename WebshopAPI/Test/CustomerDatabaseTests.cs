using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Tests
{
    public class CustomerDatabaseTests
    {
        [Fact]
        public void GetAll_ReturnsAllCustomers()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();
            var customerDatabase = new CustomerDB(dbConnection);

            // Act
            var customers = customerDatabase.GetAll();

            // Assert
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
            Assert.Equal(1, customers.Count());
        }
    }
}