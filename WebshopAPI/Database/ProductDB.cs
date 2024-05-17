using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ModelAPI;

namespace WebshopAPI.Database
{
    public class ProductDB : IProductDB
    {
        private readonly DatabaseConnection _dbConnection;

        public ProductDB(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM Product";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product(
                            (int)reader["ProductId"],
                            reader["ProductName"].ToString(),
                            (decimal)reader["ProductPrice"],
                            reader["ProductDescription"].ToString(),
                            reader["Stock"] != DBNull.Value ? (int)reader["Stock"] : 0
                        ));
                    }
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT * FROM Product WHERE ProductId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product(
                                (int)reader["ProductId"],
                                reader["ProductName"].ToString(),
                                (decimal)reader["ProductPrice"],
                                reader["ProductDescription"].ToString(),
                                reader["Stock"] != DBNull.Value ? (int)reader["Stock"] : 0
                            );
                        }
                    }
                }
            }

            return null;
        }

        public void Add(Product product)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query =
                    "INSERT INTO Product (ProductName, ProductPrice, ProductDescription, Stock) VALUES (@Name, @Price, @Description, @Stock)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.ProductName);
                    command.Parameters.AddWithValue("@Price", product.ProductPrice);
                    command.Parameters.AddWithValue("@Description", product.ProductDescription);
                    command.Parameters.AddWithValue("@Stock", product.Stock);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Product product)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query =
                    "UPDATE Product SET ProductName = @Name, ProductPrice = @Price, ProductDescription = @Description, Stock = @Stock WHERE ProductId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.ProductId);
                    command.Parameters.AddWithValue("@Name", product.ProductName);
                    command.Parameters.AddWithValue("@Price", product.ProductPrice);
                    command.Parameters.AddWithValue("@Description", product.ProductDescription);
                    command.Parameters.AddWithValue("@Stock", product.Stock);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM Product WHERE ProductId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
