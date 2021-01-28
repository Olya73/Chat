using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Contract.Bot;
using Contract.DTO;
using Contract.Bot.Interface;

namespace BotLibrary.Implementation
{
    public class BotManager: IBotManager
    {
        private readonly IServiceProvider _serviceProvider;

        public BotManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<string> BotOnMessage(BotMessageDTO botMessage, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var messagesBot in scope.ServiceProvider.GetServices<IMessageBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return messagesBot.OnMessage(botMessage);
        }

        public IEnumerable<string> BotOnCommand(BotMessageDTO botMessage, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var commandBot in scope.ServiceProvider.GetServices<ICommandBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return commandBot.OnCommand(botMessage);
        }

        public IEnumerable<string> BotOnEvent(string[] botNames, ActionTypes actionType, BotMessageDTO botMessage)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var eventBot in scope.ServiceProvider.GetServices<IEventBot>()
                .Where(service => botNames.Contains(service.Name) && service.AllowedActions.HasFlag(actionType)))
                    yield return eventBot.OnEvent(botMessage, actionType);

            if (actionType == ActionTypes.NewMessage)
            {
                foreach (var bot in BotOnMessage(botMessage, botNames))
                    yield return bot;
                foreach (var bot in BotOnCommand(botMessage, botNames))
                    yield return bot;
            }
        }
    }
}
