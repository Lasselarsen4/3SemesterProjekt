﻿// Order.cs
namespace ModelAPI
{
    public class Order
    {
        public Order(int orderId, DateTime orderDate, DateTime deliveryDate, decimal totalPrice, int customerId_FK)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            TotalPrice = totalPrice;
            CustomerId_FK = customerId_FK;
            OrderLines = new List<OrderLine>();
        }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId_FK { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Customer Cust { get; set; }
    }
}