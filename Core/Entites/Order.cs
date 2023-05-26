using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, ShippingAddress shippmendToAddress, DeliverMethod deliverMethod, IReadOnlyList<OrderItem> orderItem, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippmendToAddress = shippmendToAddress;
            DeliverMethod = deliverMethod;
            OrderItem = orderItem;
            SubTotal = subTotal;
        }
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ShippingAddress ShippmendToAddress { get; set; }

        public DeliverMethod DeliverMethod { get; set; }

        public IReadOnlyList<OrderItem> OrderItem { get; set; }

        public decimal SubTotal { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal GetTotal()
            => SubTotal + DeliverMethod.Price;
    }
}
