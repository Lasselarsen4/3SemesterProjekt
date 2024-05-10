using System.Linq;
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
            var dbConnection = new DatabaseConnection(); // Mock or use a testing database
            var customerDatabase = new CustomerDB(dbConnection);

            // Act
            var customers = customerDatabase.GetAll();

            // Assert
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
            Assert.Equal(1, customers.Count()); // Adjust this based on your test data
        }

        // Write similar tests for other methods like GetById, Add, Update, and Delete
    }
}