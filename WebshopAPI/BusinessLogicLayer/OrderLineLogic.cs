using ModelAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebshopAPI.BusinessLogicLayer
{
    public class OrderLineLogic : IOrderLineLogic
    {
        private readonly List<OrderLine> _orderLines = new List<OrderLine>();

        public IEnumerable<OrderLine> GetAllOrderLines()
        {
            return _orderLines;
        }

        public OrderLine GetOrderLineById(int id)
        {
            return _orderLines.FirstOrDefault(o => o.Quantity == id);
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            if (orderLine == null)
            {
                throw new ArgumentNullException(nameof(orderLine));
            }

            // Generate a unique ID for the new order line
            orderLine.Quantity = GenerateUniqueOrderLineId();
            
            _orderLines.Add(orderLine);
        }

        public void UpdateOrderLine(int id, OrderLine updatedOrderLine)
        {
            var orderLineToUpdate = _orderLines.FirstOrDefault(o => o.Quantity == id);
            if (orderLineToUpdate == null)
            {
                throw new InvalidOperationException($"Order line with quantity {id} not found");
            }

            orderLineToUpdate.Quantity = updatedOrderLine.Quantity;
        }

        public void DeleteOrderLine(int id)
        {
            var orderLineToRemove = _orderLines.FirstOrDefault(o => o.Quantity == id);
            if (orderLineToRemove == null)
            {
                throw new InvalidOperationException($"Order line with quantity {id} not found");
            }

            _orderLines.Remove(orderLineToRemove);
        }

        private int GenerateUniqueOrderLineId()
        {
            // Generate a unique ID logic
            return _orderLines.Count + 1;
        }
    }
}