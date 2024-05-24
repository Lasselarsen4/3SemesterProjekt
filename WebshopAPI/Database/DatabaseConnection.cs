using System;
using System.Data;
using System.Data.SqlClient;

namespace WebshopAPI.Database
{
    public class DatabaseConnection : IDisposable
    {
        private readonly string _serverAddress = "hildur.ucn.dk";
        private readonly int _serverPort = 1433;
        private readonly string _databaseName = "DMA-CSD-V23_10478730";
        private readonly string _userName = "DMA-CSD-V23_10478730";
        private readonly string _password = "Password1!";
        private readonly string _connectionString;

        private SqlConnection _connection = null;

        public DatabaseConnection()
        {
            _connectionString = $"Server={_serverAddress},{_serverPort};Database={_databaseName};User Id={_userName};Password={_password};";
        }

        public SqlConnection OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);

                try
                {
                    _connection.Open();
                }
                catch (SqlException e)
                {
                    LogError(e);
                    throw; // Rethrow the exception after logging
                }
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        private void LogError(SqlException exception)
        {
            // Logging the error to the console for now, consider using a logging framework
            Console.Error.WriteLine($"Could not connect to database {_databaseName} @ {_serverAddress}:{_serverPort} as user {_userName} using password ****");
            Console.WriteLine(exception.ToString());
        }

        // Implement IDisposable to ensure proper disposal
        public void Dispose()
        {
            CloseConnection();
        }
    }
}
