using WebshopAPI.Database;
using Xunit;

namespace WebshopAPI.Test
{
    public class DatabaseConnectionTest
    {
        [Fact]
        public void OpenConnection_DatabaseConnectionEstablished()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();

            // Act
            var connection = dbConnection.OpenConnection();

            // Assert
            Assert.NotNull(connection);
            Assert.Equal(System.Data.ConnectionState.Open, connection.State);
            
        }
    }
}