using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelAPI;

namespace WebshopAPI.Database
{
    public class OrderLineDB : IOrderLineDB
    {
        private readonly DatabaseConnection _dbConnection;

        public OrderLineDB(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<OrderLine> GetAll()
        {
            List<OrderLine> orderLines = new List<OrderLine>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM OrderLine";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderLines.Add(MapToOrderLine(reader));
                    }
                }
            }

            return orderLines;
        }

        public OrderLine GetById(int orderId, int productId)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT quantity, orderId_FK, productId_FK FROM OrderLine WHERE orderId_FK = @OrderId AND productId_FK = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapToOrderLine(reader);
                        }
                    }
                }
            }

            return null; // Return null if order line with specified IDs is not found
        }

        public void Add(OrderLine orderLine)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "INSERT INTO OrderLine (quantity, orderId_FK, productId_FK) VALUES (@Quantity, @OrderId, @ProductId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                    command.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                    command.Parameters.AddWithValue("@ProductId", orderLine.ProductId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(int orderId, int productId, OrderLine orderLine)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE OrderLine SET quantity = @Quantity WHERE orderId_FK = @OrderId AND productId_FK = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", orderLine.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int orderId, int productId)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM OrderLine WHERE orderId_FK = @OrderId AND productId_FK = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private OrderLine MapToOrderLine(SqlDataReader reader)
        {
            return new OrderLine
            {
                Quantity = (int)reader["quantity"],
                OrderId = (int)reader["orderId_FK"],
                ProductId = (int)reader["productId_FK"]
            };
        }
    }
}
