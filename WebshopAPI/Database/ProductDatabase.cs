using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace WebshopAPI.Database
{
    public class ProductDatabase
    {
        private readonly DatabaseConnection _dbConnection;

        public ProductDatabase(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT ProductId, ProductName, ProductPrice, ProductDescription FROM Products";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = MapProductFromReader(reader);
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "SELECT ProductId, ProductName, ProductPrice, ProductDescription FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = MapProductFromReader(reader);
                        }
                    }
                }
            }

            return product;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "INSERT INTO Products (ProductName, ProductPrice, ProductDescription) VALUES (@ProductName, @ProductPrice, @ProductDescription)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "UPDATE Products SET ProductName = @ProductName, ProductPrice = @ProductPrice, ProductDescription = @ProductDescription WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection connection = _dbConnection.OpenConnection())
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private Product MapProductFromReader(SqlDataReader reader)
        {
            return new Product
            {
                ProductId = Convert.ToInt32(reader["ProductId"]),
                ProductName = reader["ProductName"].ToString(),
                ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                ProductDescription = reader["ProductDescription"].ToString()
            };
        }
    }
}
