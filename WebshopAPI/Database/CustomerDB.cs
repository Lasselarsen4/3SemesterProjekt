using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelAPI;

namespace WebshopAPI.Database
{
    public class CustomerDB : ICustomerDB
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
                string query = "SELECT * FROM Customer";

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

        public Customer GetById(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT CustomerId, FirstName, LastName, Email, Phone FROM Customer WHERE CustomerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = (int)reader["CustomerId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = Convert.ToInt32(reader["Phone"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Customer customer)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "INSERT INTO Customer (FirstName, LastName, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Customer customer)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone WHERE CustomerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", customer.CustomerId);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM Customer WHERE CustomerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
