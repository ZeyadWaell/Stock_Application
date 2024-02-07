using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Stock : BaseEntity
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }

        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        //public ICollection<Orders> Orders { get; set; }

    }
}
