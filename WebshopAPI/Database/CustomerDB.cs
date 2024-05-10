using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace WebshopAPI.Database
{
    public class CustomerDB
    {
        private readonly DatabaseConnection _dbConnection;

        public CustomerDB(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT CustomerId, FirstName, LastName, Email, Phone FROM Customer";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = (int)reader["CustomerId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = Convert.ToInt32(reader["Phone"])
                        });
                    }
                }
            }

            return customers;
        }


        // Implement other methods like GetById, Add, Update, and Delete similarly
    }
}