using AutoMapper;
using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using DataAccess.Model;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary.Implementation
{
    public class BotNotifier: IBotNotifier
    {
        private readonly IBotManager _botManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public BotNotifier(IMapper mapper,
            IBotManager botManager,
            IServiceScopeFactory serviceScopeFactory)
        {
            _mapper = mapper;
            _botManager = botManager;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void NotifyAsync()
        {            
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _chatEventRepository = scope.ServiceProvider.GetService<IChatEventRepository>();
                var _chatActionRepository = scope.ServiceProvider.GetService<IBotActionOnEventRepository>();
                var _botRepository = scope.ServiceProvider.GetService<IBotRepository>();
                var context = scope.ServiceProvider.GetService<ChatNpgSQLContext>();

                ChatEvent chatEvent;
                while ((chatEvent = await _chatEventRepository.FindNewAndUpdateAsync()) != null)
                {
                    string[] names = await _botRepository.GetBotsNamesByDialogIdAsync(chatEvent.DialogId);

                    ActionTypes actionType = (ActionTypes)chatEvent.TypeOfActionId;
                    ChatEventGetDTO chatEventGetDTO = _mapper.Map<ChatEventGetDTO>(chatEvent);
                    foreach (var response in _botManager.BotOnEvent(chatEventGetDTO, names, actionType))
                    {

                        if (!String.IsNullOrEmpty(response.Message))
                        {
                            BotActionOnEvent botAction = new BotActionOnEvent()
                            {
                                ChatEvent = chatEvent,
                                BotResponse = response.Message,
                                BotName = response.BotName
                            };
                            _chatActionRepository.Add(botAction);
                        }
                    }
                    chatEvent.State = 2;
                    await context.SaveChangesAsync();
                }
            });
        }
    }
}
