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
                string query = "SELECT quantity, orderId_FK, productId_FK FROM OrderLine";

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

        public OrderLine GetById(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT quantity, orderId_FK, productId_FK FROM OrderLine WHERE orderLineId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapToOrderLine(reader);
                        }
                    }
                }
            }

            return null; // Return null if order line with specified ID is not found
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

        public void Update(int id, OrderLine orderLine)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE OrderLine SET quantity = @Quantity, orderId_FK = @OrderId, productId_FK = @ProductId WHERE orderLineId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                    command.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                    command.Parameters.AddWithValue("@ProductId", orderLine.ProductId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM OrderLine WHERE orderLineId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

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
