using Core.Entites;
using Core.Interface;
using Infrastraction.Data;
using Infrastraction.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Services
{
    public class OrderRepostory : GenericRepository<Orders>, IOrderRepostory
    {
        public OrderRepostory(StoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Orders> CheckingCurrentPrice(decimal price, string symbol)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Price == price && o.Stock.Symbol == symbol);

            return order;
        }

    }
}
