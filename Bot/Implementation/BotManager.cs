using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Contract.Bot;
using Contract.DTO;
using Contract.Bot.Interface;
using Microsoft.EntityFrameworkCore;


namespace BotLibrary.Implementation
{
    public class BotManager: IBotManager
    {
        private readonly IServiceProvider _serviceProvider;

        public BotManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<BotMessageDTO> BotOnMessage(MessageGetDTO messageGetDTO, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var messagesBot in scope.ServiceProvider.GetServices<IMessageBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return new BotMessageDTO()
                    {
                        Message = messagesBot.OnMessage(messageGetDTO),
                        BotName = messagesBot.Name
                    };
        }

        public IEnumerable<BotMessageDTO> BotOnCommand(MessageGetDTO messageGetDTO, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var commandBot in scope.ServiceProvider.GetServices<ICommandBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return new BotMessageDTO()
                    {
                        Message = commandBot.OnCommand(messageGetDTO),
                        BotName = commandBot.Name
                    };
        }

        public IEnumerable<BotMessageDTO> BotOnEvent(MessageGetDTO messageGetDTO, string[] botNames, ActionTypes actionType)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var eventBot in scope.ServiceProvider.GetServices<IEventBot>()
                .Where(service => botNames.Contains(service.Name) && service.AllowedActions.HasFlag(actionType)))
                    yield return new BotMessageDTO()
                    {
                        Message = eventBot.OnEvent(messageGetDTO, actionType),
                        BotName = eventBot.Name
                    };


            if (actionType == ActionTypes.NewMessage)
            {
                foreach (var bot in BotOnMessage(messageGetDTO, botNames))
                    yield return bot;
            }
            else if (actionType == ActionTypes.NewCommand)
            {
                foreach (var bot in BotOnCommand(messageGetDTO, botNames))
                    yield return bot;
            }
                
        }
    }
}
