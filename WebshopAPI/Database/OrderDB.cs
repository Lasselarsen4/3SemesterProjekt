using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelAPI;

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
                        var order = new Order(
                            (int)reader["orderId"],
                            (DateTime)reader["orderDate"],
                            (DateTime)reader["deliveryDate"],
                            (decimal)reader["totalPrice"],
                            (int)reader["customerId_FK"]
                        );
                        orders.Add(order);
                    }
                }

                // Retrieve order lines for all orders
                foreach (var order in orders)
                {
                    order.OrderLines = GetOrderLinesByOrderId(order.OrderId, connection);
                }
            }

            return orders;
        }

        public Order GetById(int id)
        {
            Order order = null;

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
                            order = new Order(
                                (int)reader["orderId"],
                                (DateTime)reader["orderDate"],
                                (DateTime)reader["deliveryDate"],
                                (decimal)reader["totalPrice"],
                                (int)reader["customerId_FK"]
                            );
                        }
                    }
                }

                if (order != null)
                {
                    order.OrderLines = GetOrderLinesByOrderId(order.OrderId, connection);
                }
            }

            return order;
        }

        private List<OrderLine> GetOrderLinesByOrderId(int orderId, SqlConnection connection)
        {
            List<OrderLine> orderLines = new List<OrderLine>();

            string query = @"
                SELECT ol.orderId_FK, ol.productId_FK, ol.quantity, 
                       p.productId, p.productName, p.productPrice, p.productDescription
                FROM OrderLine ol
                JOIN Product p ON ol.productId_FK = p.productId
                WHERE ol.orderId_FK = @OrderId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderId", orderId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var orderLine = new OrderLine
                        {
                            OrderId = (int)reader["orderId_FK"],
                            ProductId = (int)reader["productId_FK"],
                            Quantity = (int)reader["quantity"],
                            Product = new Product
                            {
                                ProductId = (int)reader["productId"],
                                ProductName = (string)reader["productName"],
                                ProductPrice = (decimal)reader["productPrice"],
                                ProductDescription = (string)reader["productDescription"]
                            }
                        };
                        orderLines.Add(orderLine);
                    }
                }
            }

            return orderLines;
        }

        public void Add(Order order)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO [Order] (orderDate, deliveryDate, totalPrice, customerId_FK) OUTPUT INSERTED.orderId VALUES (@OrderDate, @DeliveryDate, @TotalPrice, @CustomerId)";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                            command.Parameters.AddWithValue("@DeliveryDate", order.DeliveryDate);
                            command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);

                            order.OrderId = (int)command.ExecuteScalar();
                        }

                        foreach (var orderLine in order.OrderLines)
                        {
                            orderLine.OrderId = order.OrderId;
                            string orderLineQuery = "INSERT INTO OrderLine (quantity, orderId_FK, productId_FK) VALUES (@Quantity, @OrderId, @ProductId)";

                            using (SqlCommand command = new SqlCommand(orderLineQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                                command.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                                command.Parameters.AddWithValue("@ProductId", orderLine.ProductId);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
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
