using System;
using System.Threading.Tasks;
using Tinkoff.Infrastructure.Core;

namespace Tinkoff.Service.SimpleChat
{
    /// <inheritdoc />
    public class SimpleChatService : ISimpleChatService
    {
        private readonly ICacheStore _cacheStore;
        private const string PrefixKey = "chat.history.";

        public SimpleChatService(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        /// <inheritdoc />
        public async Task JoinChatRoom(string channel, string userName, Action<string> subHandler)
        {
            await _cacheStore.SubscribeAsync(channel, subHandler);
            var history = await _cacheStore.GetAllAsync($"{PrefixKey}*");
            subHandler(string.Join("\n", history));
            await _cacheStore.PublishAsync(channel, $"'{userName}' joined the chat room.");
        }

        /// <inheritdoc />
        public async Task MessageExchange(string channel, string userName, string message)
        {
            var key = $"{PrefixKey}{DateTime.Now}";
            var fullMessage = $"{userName}: {message} ({DateTime.Now.Hour}:{DateTime.Now.Minute})";
            await _cacheStore.SetAsync(key, fullMessage);
            await _cacheStore.PublishAsync(channel, fullMessage);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _cacheStore?.Dispose();
        }
    }
}
