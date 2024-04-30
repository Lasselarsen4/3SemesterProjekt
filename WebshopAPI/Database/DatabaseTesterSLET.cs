using System;
using System.Data;
using System.Data.SqlClient;
using WebshopAPI.Database;

namespace WebshopAPI
{
    class DatabaseTesterSLET
    {
        static void Main(string[] args)
        {
            TestDatabaseConnection();
        }

        static void TestDatabaseConnection()
        {
            // Get an instance of the DatabaseConnection
            DatabaseConnection dbConnection = DatabaseConnection.GetInstance();

            try
            {
                // Open the database connection
                SqlConnection connection = dbConnection.OpenConnection();

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Database connection opened successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to open database connection.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Close the database connection
                dbConnection.CloseConnection();
                Console.WriteLine("Database connection closed.");
            }
        }
    }
}