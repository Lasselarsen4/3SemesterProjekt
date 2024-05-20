using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelAPI;

namespace WebshopAPI.Database
{
    public class CartDB : ICartDB
    {
        private readonly DatabaseConnection _dbConnection;

        public CartDB(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Cart> GetAll()
        {
            List<Cart> carts = new List<Cart>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM Cart";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carts.Add(new Cart
                        {
                            CartId = (int)reader["cartId"],
                            CustomerId = (int)reader["customerId"],
                            CreatedAt = (DateTime)reader["createdAt"]
                            // Add logic to retrieve CartItems if necessary
                        });
                    }
                }
            }

            return carts;
        }

        public Cart GetById(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM Cart WHERE cartId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cart
                            {
                                CartId = (int)reader["cartId"],
                                CustomerId = (int)reader["customerId"],
                                CreatedAt = (DateTime)reader["createdAt"]
                                // Add logic to retrieve CartItems if necessary
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Cart cart)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "INSERT INTO Cart (customerId, createdAt) VALUES (@CustomerId, @CreatedAt)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", cart.CustomerId);
                    command.Parameters.AddWithValue("@CreatedAt", cart.CreatedAt);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Cart cart)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE Cart SET customerId = @CustomerId, createdAt = @CreatedAt WHERE cartId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", cart.CartId);
                    command.Parameters.AddWithValue("@CustomerId", cart.CustomerId);
                    command.Parameters.AddWithValue("@CreatedAt", cart.CreatedAt);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM Cart WHERE cartId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
