﻿namespace ModelAPI
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }

        public Product(int productId, string name, decimal price, string description)
        {
            ProductId = productId;
            ProductName = name;
            ProductPrice = price;
            ProductDescription = description;
        }
    }
}