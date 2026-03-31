using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<string> GetValueAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public void Dispose() => _connection.Dispose();
    }
}
