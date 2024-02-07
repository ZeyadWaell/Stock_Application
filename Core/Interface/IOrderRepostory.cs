using Core.Entites;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IOrderRepostory : IGenericRepository<Orders>
    {
        Task<Orders> CheckingCurrentPrice(decimal price, string symbol);
    }
}
