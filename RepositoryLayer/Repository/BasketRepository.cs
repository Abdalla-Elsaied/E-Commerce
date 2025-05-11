using DomainLayer.Entities;
using DomainLayer.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class BasketRepository : IBasketRepository
    {
        public IDatabase _RedisDb { get; }
        public BasketRepository(IConnectionMultiplexer RedisDb)
        {
            _RedisDb = RedisDb.GetDatabase();
        }


        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _RedisDb.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
           var basket= await _RedisDb.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null :JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
          var CreatedOrUpdated = await _RedisDb.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (CreatedOrUpdated is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
