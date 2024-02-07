using Core.Entites.Enum;
using Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Orders : BaseEntity
    {
        public OrderType? OrderType { get; set; } 
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
        public Guid StockId { get; set; }

        [ForeignKey(nameof(StockId))]
        public Stock Stock { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser AppUsers { get; set; }

        public OrderStatue Status { get; set; }
    }
}
