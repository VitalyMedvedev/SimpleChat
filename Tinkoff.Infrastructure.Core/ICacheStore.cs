using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tinkoff.Infrastructure.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Storage logic
    /// </summary>
    public interface ICacheStore: IDisposable
    {
        /// <summary>
        /// Subscribe to perform some operation when a change to the preferred/active node is broadcast
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        Task SubscribeAsync(string channel, Action<string> handler);

        /// <summary>
        /// Posts a message to the given channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task PublishAsync(string channel, string message);

        /// <summary>
        /// Set key to hold the string value. If key already holds a value, it is overwritten, regardless of its type
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task SetAsync(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// Get the value of key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// Get values by pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        Task<List<string>> GetAllAsync(string pattern);
    }
}
