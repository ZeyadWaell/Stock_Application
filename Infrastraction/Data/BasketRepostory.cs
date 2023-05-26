using Core.Entites;
using Core.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class BasketRepostory : IBasketRepostoryt
    {
        private readonly IDatabase _database;
        public BasketRepostory(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public Task DeleteBasketAsynbc(string Id)
      => _database.KeyDeleteAsync(Id);  

        public async Task<CustumerBasket> GetBasketAsync(string Id)
        {
            var data = await _database.StringGetAsync(Id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustumerBasket>(data);
        }

        public async Task<CustumerBasket> UpdateBasketAsync(CustumerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromHours(1));

            if (!created)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
