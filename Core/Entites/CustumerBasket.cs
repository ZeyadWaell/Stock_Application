using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class CustumerBasket
    {
        public CustumerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; }

        public int? DeleiverMethod { get; set; }

        public decimal ShippingPrice { get; set; }

        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
