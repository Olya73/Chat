using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Contract.Bot;
using Contract.DTO;
using Contract.Bot.Interface;
using Microsoft.EntityFrameworkCore;
using DataAccess.Model;
using System.Threading.Tasks;
using Repository.Storage.Interface;
using Contract.ConfigurationModel;
using Microsoft.Extensions.Options;
using AutoMapper;
using Repository;

namespace BotLibrary.Implementation
{
    public class BotManager: IBotManager
    {
        private readonly IServiceProvider _serviceProvider;

        public BotManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<BotMessageDTO> BotOnMessage(ChatEventGetDTO chatEventGetDTO, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var messagesBot in scope.ServiceProvider.GetServices<IMessageBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return new BotMessageDTO()
                    {
                        Message = messagesBot.OnMessage(chatEventGetDTO),
                        BotName = messagesBot.Name
                    };
        }

        public IEnumerable<BotMessageDTO> BotOnCommand(ChatEventGetDTO chatEventGetDTO, string[] botNames)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var commandBot in scope.ServiceProvider.GetServices<ICommandBot>()
                .Where(service => botNames.Contains(service.Name)))
                    yield return new BotMessageDTO()
                    {
                        Message = commandBot.OnCommand(chatEventGetDTO),
                        BotName = commandBot.Name
                    };
        }

        public IEnumerable<BotMessageDTO> BotOnEvent(ChatEventGetDTO chatEventGetDTO, string[] botNames, ActionTypes actionType)
        {
            using var scope = _serviceProvider.CreateScope();
            foreach (var eventBot in scope.ServiceProvider.GetServices<IEventBot>()
                .Where(service => botNames.Contains(service.Name) && service.AllowedActions.HasFlag(actionType)))
                    yield return new BotMessageDTO()
                    {
                        Message = eventBot.OnEvent(chatEventGetDTO, actionType),
                        BotName = eventBot.Name
                    };

            if (actionType == ActionTypes.NewMessage)
            {
                foreach (var bot in BotOnMessage(chatEventGetDTO, botNames))
                    yield return bot;
            }
            else if (actionType == ActionTypes.NewCommand)
            {
                foreach (var bot in BotOnCommand(chatEventGetDTO, botNames))
                    yield return bot;
            }                
        }

        
    }
}
