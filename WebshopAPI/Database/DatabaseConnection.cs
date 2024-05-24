using System.Data.SqlClient;

public class DatabaseConnection : IDisposable
{
    private readonly string _serverAddress = "hildur.ucn.dk";
    private readonly int _serverPort = 1433;
    private readonly string _databaseName = "DMA-CSD-V23_10478730";
    private readonly string _userName = "DMA-CSD-V23_10478730";
    private readonly string _password = "Password1!";
    private readonly string _connectionString;

    public DatabaseConnection()
    {
        _connectionString = $"Server={_serverAddress},{_serverPort};Database={_databaseName};User Id={_userName};Password={_password};";
    }

    public SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(_connectionString);

        try
        {
            connection.Open();
        }
        catch (SqlException e)
        {
            LogError(e);
            throw; // Rethrow the exception after logging
        }

        return connection;
    }

    private void LogError(SqlException exception)
    {
        // Logging the error to the console for now, consider using a logging framework
        Console.Error.WriteLine($"Could not connect to database {_databaseName} @ {_serverAddress}:{_serverPort} as user {_userName} using password ****");
        Console.WriteLine(exception.ToString());
    }

    public void Dispose()
    {
        // No longer managing a single connection, so nothing to dispose here
    }
}