using System;
using System.Data;
using System.Data.SqlClient;
using WebshopAPI.Database;

namespace WebshopAPI
{
    class DatabaseTester
    {
        public static void TestDatabase()
        {
            try
            {
                // Get an instance of the DatabaseConnection
                DatabaseConnection dbConnection = DatabaseConnection.GetInstance();

                // Open the database connection
                using (SqlConnection connection = dbConnection.OpenConnection())
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Database connection opened successfully.");

                        // Example query: select all rows from the TestTable
                        string query = "SELECT * FROM TestTable";

                        // Create a SqlCommand object with the query and connection
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Execute the query and retrieve the results
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Iterate over the results and print them
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0); // Assuming the Id column is of type int
                                    string name = reader.GetString(1); // Assuming the Name column is of type string
                                    Console.WriteLine($"Id: {id}, Name: {name}");
                                }
                            }
                        }

                        // Close the database connection
                        dbConnection.CloseConnection();
                        Console.WriteLine("Database connection closed.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to open database connection.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
