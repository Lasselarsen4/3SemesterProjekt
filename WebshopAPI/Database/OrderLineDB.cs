using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

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
            // Implement GetById method logic here
            throw new NotImplementedException();
        }

        public void Add(OrderLine orderLine)
        {
            // Implement Add method logic here
            throw new NotImplementedException();
        }

        public void Update(OrderLine orderLine)
        {
            // Implement Update method logic here
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            // Implement Delete method logic here
            throw new NotImplementedException();
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
