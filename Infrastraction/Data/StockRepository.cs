using Core.Entites;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        public StockRepository(StoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
