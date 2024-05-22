using System;
using System.Collections.Generic;

namespace WebshopApplication.Models
{
    public class Order
    {
        public Customer Cust { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId_FK { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}


