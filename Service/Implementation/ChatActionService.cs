using AutoMapper;
using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using DataAccess.Model;
using Repository;
using Repository.Storage.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ChatActionService: IChatActionService
    {
        private readonly ChatNpgSQLContext _context;
        private readonly IMapper _mapper;
        private readonly IBotManager _botManager;
        private readonly IChatEventRepository _chatEventRepository;
        private readonly IBotRepository _botRepository;
        private readonly IBotActionOnEventRepository _chatActionRepository;

        public ChatActionService(ChatNpgSQLContext context, 
            IMapper mapper, 
            IBotManager botManager, 
            IChatEventRepository chatEventRepository,
            IBotRepository botRepository,
            IBotActionOnEventRepository chatActionRepository)
        {
            _context = context;
            _mapper = mapper;
            _botManager = botManager;
            _chatEventRepository = chatEventRepository;
            _botRepository = botRepository;
            _chatActionRepository = chatActionRepository;
        }

        public async Task AddChatEvent(ChatEventDTO chatEventDTO)
        {
            ChatEvent chat = _mapper.Map<ChatEvent>(chatEventDTO);
            _chatEventRepository.Add(chat);
            await _context.SaveChangesAsync();   
        }

        public async Task NotifyAsync()
        {
            //if (repos == null) throw new ();
            ChatEvent chatEvent;
            while ((chatEvent = _chatEventRepository.FindNewAndUpdate()) != null)
            {
                string[] names = await _botRepository.GetBotsNamesByDialogIdAsync(chatEvent.DialogId);

                ActionTypes actionType = (ActionTypes)Enum.Parse(typeof(ActionTypes), chatEvent.TypeOfAction.ToString(), true);
                MessageGetDTO messageGetDTO = _mapper.Map<MessageGetDTO>(chatEvent.Message);
                foreach (var response in _botManager.BotOnEvent(messageGetDTO, names, actionType))
                {
                    if (!String.IsNullOrEmpty(response.Message))
                    {
                        BotActionOnEvent chatAction = new BotActionOnEvent()
                        {
                            ChatEvent = chatEvent,
                            BotResponse = response.Message,
                            BotName = response.BotName
                        };
                        _chatActionRepository.Add(chatAction);
                        await _context.SaveChangesAsync();
                    }
                }
                _chatEventRepository.Update(chatEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
