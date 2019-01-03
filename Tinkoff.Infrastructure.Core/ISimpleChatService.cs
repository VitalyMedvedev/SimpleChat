using System;
using System.Threading.Tasks;

namespace Tinkoff.Infrastructure.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Chat functionality logic
    /// </summary>
    public interface ISimpleChatService: IDisposable
    {
        /// <summary>
        /// Joining the chat room
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="userName"></param>
        /// <param name="subHandler"></param>
        /// <returns></returns>
        Task JoinChatRoom(string channel, string userName, Action<string> subHandler);

        /// <summary>
        /// The method that implements the logic of messaging
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task MessageExchange(string channel, string userName, string message);
    }
}
