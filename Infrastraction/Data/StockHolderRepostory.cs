using Core.Entites;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class StockHolderRepostory : GenericRepository<StockHolder>, IStockHolderRepostory
    {
        public StockHolderRepostory(StoreDbContext dbContext) : base(dbContext)
        {
        }

    }
}
