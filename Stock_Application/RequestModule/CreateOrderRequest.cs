using Core.Entites.Enum;
using Core.Entites.Identity;
using Core.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Application.RequestModule
{
    public class CreateOrderRequest
    {
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public OrderType OrderType { get; set; }
    }
}
