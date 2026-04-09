using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.database
{
    public class RedisService : IDisposable
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public RedisService(string connectionString = "localhost:6379")
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _db = _connection.GetDatabase();
        }

        public void Dispose() => _connection.Dispose();

        public async Task<string> GetValueAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task SetValueAsync(string key, string value)
        {
            await _db.StringSetAsync(key, value);
        }

        public async Task SubscribeAsync(string channel, Action<string> handler)
        {
            var subscriber = _connection.GetSubscriber();
            await subscriber.SubscribeAsync(channel, (ch, message) => handler(message));
        }

        public async Task PublishAsync(string channel, string message)
        {
            var subscriber = _connection.GetSubscriber();
            await subscriber.PublishAsync(channel, message);
        }
    }
}
