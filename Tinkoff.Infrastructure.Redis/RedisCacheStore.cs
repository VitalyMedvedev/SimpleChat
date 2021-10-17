using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Infrastructure.Core;

namespace Tinkoff.Infrastructure.Redis
{
    /// <inheritdoc />
    public class RedisCacheStore : ICacheStore
    {
        private readonly ISubscriber _subscriber;
        private readonly IDatabase _database;
        private readonly IServer _server;
        private readonly IRedisServerSettings _settings;
        private readonly IRedisConnection _connection;

        public RedisCacheStore(IRedisConnection connection, IRedisServerSettings settings)
        {
            _connection = connection;
            _subscriber = _connection.GetSubscriber();
            _database = _connection.GetDatabase();
            _server = _connection.GetServer(_connection.GetEndPoints().FirstOrDefault());
            _settings = settings;
        }

        /// <inheritdoc />
        public async Task SubscribeAsync(string channel, Action<string> handler)
        {
            await _subscriber.SubscribeAsync(channel, (channelValue, message) => handler(message)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task PublishAsync(string channel, string message)
        {
            await _subscriber.PublishAsync(channel, message).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry ?? _settings.Expiry).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<string> GetAsync(string key)
        {
            var result = await _database.StringGetAsync(key).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<List<string>> GetAllAsync(string pattern)
        {
            var result = new List<string>();
            var keys = _server.Keys(pattern: pattern).ToArray();
            var sortKeys = keys.OrderBy(x => x.ToString()).ToList();
            foreach(var key in sortKeys)
            {
                result.Add(await GetAsync(key).ConfigureAwait(false));
            }
            return result;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
