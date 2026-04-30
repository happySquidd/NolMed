using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // convert to singleton
        private static RedisService _instance;
        public static RedisService Instance => _instance ?? new RedisService();
        private readonly ISubscriber _subscriber;

        private RedisService(string connectionString = "localhost:6379")
        {
            try
            {
                _connection = ConnectionMultiplexer.Connect(connectionString);
                _db = _connection.GetDatabase();
                _subscriber = _connection.GetSubscriber();
            }
            catch
            {
                Debug.WriteLine("redis unavailable");
            }
        }

        public void Dispose() => _connection.Dispose();

        public async Task SubscribeToRoom(int roomId, Action<string> handler)
        {
            // opens a subscription and follows it, returning exact room number's heart rate reading
            string channelName = $"emergency:rooms:{roomId}:ekg";
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);
            var subscriber = _connection.GetSubscriber();
            subscriber.Subscribe(channel, (ch, message) =>
            {
                handler?.Invoke(message);
            });
        }

        public void UnsubscribeFromRoom(string roomId)
        {
            string channelName = $"emergency:rooms:{roomId}:ekg";
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);
            var subscriber = _connection.GetSubscriber();
            subscriber.Unsubscribe(channel);
        }

        public async Task SubscribeToAllRooms(Action<string> handler)
        {
            // starts a subscriber connection and connects to all emergency rooms
            // use for the emergency rooms overview tab
            string channelName = "emergency:rooms:all";
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);
            var subscriber = _connection.GetSubscriber();
            subscriber.Subscribe(channel, (ch, message) =>
            {
                handler?.Invoke(message);
            });
        }
    }
}
