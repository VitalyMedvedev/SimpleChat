using System;
using Tinkoff.Infrastructure.Core;
using Tinkoff.Infrastructure.Redis;
using Tinkoff.Service.SimpleChat;
using Unity;

namespace Tinkoff.Application.ConsoleSimpleChat
{
    internal class Program
    {
        private static IUnityContainer _container;
        private const string ChatChannel = "Chat-Simple-Channel";
        private static string _userName = string.Empty;

        private static void Main()
        {
            InitializeIocContainer();
            Console.Write("Enter your name: ");
            _userName = Console.ReadLine();

            var service = _container.Resolve<ISimpleChatService>();
            var wasJoined = false;
            while (true)
            {
                if (!wasJoined)
                {
                    service.JoinChatRoom(ChatChannel, _userName, Console.WriteLine);
                    wasJoined = true;
                }
                var message = Console.ReadLine();
                service.MessageExchange(ChatChannel, _userName, message).Wait();
            }
        }

        private static void InitializeIocContainer()
        {
            _container = new UnityContainer();
            _container.RegisterType<IRedisConnection, RedisConnection>();
            _container.RegisterType<IRedisServerSettings, RedisServerSettings>();
            _container.RegisterType<ICacheStore, RedisCacheStore>();
            _container.RegisterType<ISimpleChatService, SimpleChatService>();
        }
    }
}
