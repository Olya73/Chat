using BotLibrary.Inteface;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BotLibrary.Implementation
{
    public class BotManager: IBotManager
    {
        private readonly IServiceProvider _serviceProvider;

        public BotManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<string> BotOnMessage(string[] botsNames, string actionType)
        {
            using var scope = _serviceProvider.CreateScope();
            if (actionType == "NewMessage")
            {
                foreach (var messagesBot in scope.ServiceProvider.GetServices<IMessageBot>()
                .Where(service => botsNames.Contains(service.Name)))//service.DialogIds.Contains(messageAddDTO.DialogId)))
                    yield return messagesBot.OnMessage("message");
                foreach (var commandBot in scope.ServiceProvider.GetServices<ICommandBot>()
                .Where(service => botsNames.Contains(service.Name)))
                    yield return commandBot.OnCommand("command");
            }
            else
            {
                foreach (var eventBot in scope.ServiceProvider.GetServices<IEventBot>()
                .Where(service => botsNames.Contains(service.Name)))
                    yield return eventBot.OnEvent("event");
            }
        }
    }
}
