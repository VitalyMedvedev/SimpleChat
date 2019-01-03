using System;
using System.Configuration;

namespace Tinkoff.Infrastructure.Redis
{
    /// <inheritdoc />
    public class RedisServerSettings : IRedisServerSettings
    {
        /// <inheritdoc />
        public string ConnectionString => ConfigurationManager.ConnectionStrings["RedisServer"].ConnectionString;
        /// <inheritdoc />
        public TimeSpan? Expiry
        {
            get
            {
                var expiry = ConfigurationManager.AppSettings["Expiry"];
                if (!TimeSpan.TryParse(expiry, out var result))
                {
                    return null;
                }
                return result;
            }
        }
    }
}
