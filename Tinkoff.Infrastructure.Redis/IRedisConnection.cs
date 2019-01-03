using System;
using StackExchange.Redis;
using System.Net;

namespace Tinkoff.Infrastructure.Redis
{
    /// <inheritdoc />
    /// <summary>
    /// Connection logic
    /// </summary>
    public interface IRedisConnection: IDisposable
    {
        /// <summary>
        /// Obtain a pub/sub subscriber connection to the specified server
        /// </summary>
        /// <returns></returns>
        ISubscriber GetSubscriber();

        /// <summary>
        /// Obtain an interactive connection to a database inside redis
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();

        /// <summary>
        /// Obtain a configuration API for an individual server
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        IServer GetServer(EndPoint endPoint);

        /// <summary>
        /// Gets all endpoints defined on the server
        /// </summary>
        /// <returns></returns>
        EndPoint[] GetEndPoints();
    }
}
