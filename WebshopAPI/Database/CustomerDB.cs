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
                            CustomerId = (int)reader["customerId"],
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Email = reader["email"].ToString(),
                            Phone = Convert.ToInt32(reader["phone"]),
                            StreetName = reader["streetName"].ToString(),
                            HouseNumber = (int)reader["houseNumber"],
                            ZipCode = (int)reader["zipCode"]
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
                string query = "SELECT * FROM Customer WHERE customerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = (int)reader["customerId"],
                                FirstName = reader["firstName"].ToString(),
                                LastName = reader["lastName"].ToString(),
                                Email = reader["email"].ToString(),
                                Phone = Convert.ToInt32(reader["phone"]),
                                StreetName = reader["streetName"].ToString(),
                                HouseNumber = (int)reader["houseNumber"],
                                ZipCode = (int)reader["zipCode"]
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
                string query = "INSERT INTO Customer (firstName, lastName, email, phone, streetName, houseNumber, zipCode) VALUES (@FirstName, @LastName, @Email, @Phone, @StreetName, @HouseNumber, @ZipCode)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@StreetName", customer.StreetName);
                    command.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                    command.Parameters.AddWithValue("@ZipCode", customer.ZipCode);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Customer customer)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE Customer SET firstName = @FirstName, lastName = @LastName, email = @Email, phone = @Phone, streetName = @StreetName, houseNumber = @HouseNumber, zipCode = @ZipCode WHERE customerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", customer.CustomerId);
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@StreetName", customer.StreetName);
                    command.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                    command.Parameters.AddWithValue("@ZipCode", customer.ZipCode);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM Customer WHERE customerId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
