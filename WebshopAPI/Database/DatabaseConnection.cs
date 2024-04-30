
using System;
using System.Data;
using System.Data.SqlClient;

namespace WebshopAPI.Database
{
    public class DatabaseConnection
    {
        // Singleton instance of DatabaseConnection
        private static DatabaseConnection instance;

        // SQL Server connection and database information
        private SqlConnection connection = null;
        private static readonly string serverAddress = "hildur.ucn.dk";
        private static readonly string databaseName = "DMA-CSD-V23_10478742";
        private static readonly string userName = "DMA-CSD-V23_10478742";
        private static readonly string password = "Password1!";
        private static readonly int serverPort = 1433;

        // Private constructor to create the SQL Server connection
        private DatabaseConnection()
        {
            string connectionString = $"Server={serverAddress},{serverPort};Database={databaseName};User Id={userName};Password={password};";

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException e)
            {
                Console.Error.WriteLine($"Could not connect to database {databaseName} @ {serverAddress}:{serverPort} as user {userName} using password ****");
                Console.WriteLine($"Connection string was: {connectionString.Replace(password, "****")}");
                Console.WriteLine(e.ToString());
            }
        }

        // Singleton method to get an instance of DatabaseConnection
        public static DatabaseConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseConnection();
            }
            return instance;
        }

        // Starts a database transaction
        public void StartTransaction()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.BeginTransaction();
            }
            else
            {
                throw new InvalidOperationException("Connection is not open.");
            }
        }

        // Commits a database transaction
        public void CommitTransaction()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Commit();
            }
            else
            {
                throw new InvalidOperationException("Connection is not open.");
            }
        }

        // Rolls back a database transaction
        public void RollbackTransaction()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Rollback();
            }
            else
            {
                throw new InvalidOperationException("Connection is not open.");
            }
        }

        // Executes an insert query and returns the generated identity value
        public int ExecuteInsertWithIdentity(SqlCommand cmd)
        {
            int res = -1;
            try
            {
                res = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        // Executes an update query
        public int ExecuteUpdate(SqlCommand cmd)
        {
            int res = -1;
            try
            {
                res = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        // Returns the SQL Server connection
        public SqlConnection GetConnection()
        {
            return connection;
        }

        // Disconnects from the SQL Server database
        public void Disconnect()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
