using Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class StockHolder : BaseEntity
    {
        public Guid StockId { get; set; }
        [ForeignKey(nameof(StockId))]
        public Stock Stock { get;set; }

        public int Quantity { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}
