using System;

namespace Model
{
    public class Order
    {
        public Order(int orderId, DateTime orderDate, DateTime deliveryDate, decimal totalPrice)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            TotalPrice = totalPrice;
            DeliveryDate = deliveryDate;
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}