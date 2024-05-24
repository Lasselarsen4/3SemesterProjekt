using System;
using System.Data;
using System.Threading;
using NUnit.Framework;
using WebshopAPI.BusinessLogicLayer;
using WebshopAPI.Database;

namespace WebshopAPI.Test
{
    [TestFixture]
    public class ConcurrencyTests
    {
        private DatabaseConnection _dbConnection;

        [SetUp]
        public void Setup()
        {
            _dbConnection = new DatabaseConnection();
        }

        [Test]
        public void TestConcurrencyOnProductUpdate()
        {
            var productDB = new ProductDB(_dbConnection);
            var product = productDB.GetById(1); // Assuming a product with ID 1 exists
            var originalStock = product.Stock;
            var rowVersion = product.RowVersion;

            var thread1 = new Thread(() => UpdateProductStock(1, 1, rowVersion));
            var thread2 = new Thread(() => UpdateProductStock(1, 1, rowVersion));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            var updatedProduct = productDB.GetById(1);

            if (updatedProduct.Stock == originalStock - 2)
            {
                Console.WriteLine("Concurrency issue fixed.");
            }
            else
            {
                Console.WriteLine("Concurrency issue detected.");
            }

            Assert.That(updatedProduct.Stock, Is.EqualTo(originalStock - 2));
        }

        private void UpdateProductStock(int productId, int quantity, byte[] rowVersion)
        {
            var productDB = new ProductDB(_dbConnection);

            try
            {
                productDB.UpdateProductStock(productId, quantity, rowVersion);
            }
            catch (DBConcurrencyException)
            {
                // Handle concurrency exception
                Console.WriteLine("Concurrency conflict detected.");
            }
        }
    }
}