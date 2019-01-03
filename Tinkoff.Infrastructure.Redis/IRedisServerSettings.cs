using System;

namespace Tinkoff.Infrastructure.Redis
{
    /// <summary>
    /// Server settings
    /// </summary>
    public interface IRedisServerSettings
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Data expiration time
        /// </summary>
        TimeSpan? Expiry { get; }
    }
}
