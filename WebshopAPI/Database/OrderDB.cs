using ModelAPI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebshopAPI.Database
{
    public class OrderDB : IOrderDB
    {
        private readonly DatabaseConnection _dbConnection;

        public OrderDB(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM [Order]";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order(
                            (int)reader["orderId"],
                            (DateTime)reader["orderDate"],
                            (DateTime)reader["deliveryDate"],
                            (decimal)reader["totalPrice"],
                            (int)reader["customerId_FK"]
                        ));
                    }
                }
            }

            return orders;
        }

        public Order GetById(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM [Order] WHERE orderId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Order(
                                (int)reader["orderId"],
                                (DateTime)reader["orderDate"],
                                (DateTime)reader["deliveryDate"],
                                (decimal)reader["totalPrice"],
                                (int)reader["customerId_FK"]
                            );
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Order order)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "INSERT INTO [Order] (orderDate, deliveryDate, totalPrice, customerId_FK) VALUES (@OrderDate, @DeliveryDate, @TotalPrice, @CustomerId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryDate", order.DeliveryDate);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Order order)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE [Order] SET orderDate = @OrderDate, deliveryDate = @DeliveryDate, totalPrice = @TotalPrice WHERE orderId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", order.OrderId);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryDate", order.DeliveryDate);
                    command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM [Order] WHERE orderId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
