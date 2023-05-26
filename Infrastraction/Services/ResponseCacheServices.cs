using Core.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastraction.Services
{
    public class ResponseCacheServices : IResponseCasheService
    {
        private readonly IDatabase _database;
        public ResponseCacheServices(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CachResponseAsync(string casheKey, string reposonse, TimeSpan timetoLive)
        {
            if (reposonse == null)
                return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var serlizedResponse = JsonSerializer.Serialize(reposonse, options);

            await _database.StringSetAsync(casheKey, serlizedResponse,timetoLive);
        }

        public async Task<string> CachResponseAsync(string casheKey)
        {
          var cachedreposnse = await _database.StringGetAsync(casheKey);

            if (cachedreposnse.IsNullOrEmpty)
                return null;

            return cachedreposnse;
        }
    }
}
