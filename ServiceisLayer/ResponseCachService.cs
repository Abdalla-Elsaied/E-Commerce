using DomainLayer;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceisLayer
{
    public class ResponseCachService : IResponseCachService
    {
        private readonly IDatabase _database;

        public ResponseCachService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CashResponseAsync(string CachKey, object Response, TimeSpan TimeLive)
        {
            if (Response == null) return ;

            var jsonOptions=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializeResponse=JsonSerializer.Serialize(Response, jsonOptions);

            await _database.StringSetAsync(CachKey, serializeResponse,TimeLive);
          
        }

        public async Task<string?> GetCashResponseAsync(string Cashkey)
        {
           var CachedResponse= await _database.StringGetAsync(Cashkey);

            if (CachedResponse.IsNullOrEmpty) return null;

            return CachedResponse;   
        }
    }
}
