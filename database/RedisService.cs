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
        public IDatabase Db;
        public ISubscriber Subscriber;

        public RedisService(string connectionString = "localhost:6379")
        {
            try
            {
                _connection = ConnectionMultiplexer.Connect(connectionString);
                Db = _connection.GetDatabase();
                Subscriber = _connection.GetSubscriber();
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
            Subscriber.Subscribe(channel, (ch, message) =>
            {
                handler?.Invoke(message);
            });
        }

        public void UnsubscribeFromRoom(string roomId)
        {
            string channelName = $"emergency:rooms:{roomId}:ekg";
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);
            Subscriber.Unsubscribe(channel);
        }

        public async Task SubscribeToAllRooms(Action<string> handler)
        {
            // starts a subscriber connection and connects to all emergency rooms
            // use for the emergency rooms overview tab
            string channelName = "emergency:rooms:all";
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);
            Subscriber.Subscribe(channel, (ch, message) =>
            {
                handler?.Invoke(message);
            });
        }
    }
}
