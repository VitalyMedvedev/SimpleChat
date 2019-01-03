using System;
using System.Net;
using StackExchange.Redis;

namespace Tinkoff.Infrastructure.Redis
{
    /// <inheritdoc />
    public class RedisConnection : IRedisConnection
    {
        private readonly IRedisServerSettings _settings;
        private readonly Lazy<string> _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private readonly object _lock = new object();

        /// <inheritdoc />
        public RedisConnection(IRedisServerSettings settings)
        {
            _settings = settings;
            _connectionString = new Lazy<string>(GetConnectionString);
        }

        /// <inheritdoc />
        public ISubscriber GetSubscriber()
        {
            return GetConnection().GetSubscriber();
        }

        /// <inheritdoc />
        public IDatabase GetDatabase()
        {
            return GetConnection().GetDatabase();
        }

        /// <inheritdoc />
        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        /// <inheritdoc />
        public EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _connection?.Dispose();
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;
            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;
                _connection?.Dispose();
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }
            return _connection;
        }

        private string GetConnectionString()
        {
            return _settings.ConnectionString;
        }
    }
}
