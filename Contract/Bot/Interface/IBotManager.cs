using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Bot.Interface
{
    public interface IBotManager
    {
        IEnumerable<BotMessageDTO> BotOnCommand(ChatEventGetDTO chatEventGetDTO, string[] botNames);
        IEnumerable<BotMessageDTO> BotOnEvent(ChatEventGetDTO chatEventGetDTO, string[] botNames, ActionTypes actionType);
        IEnumerable<BotMessageDTO> BotOnMessage(ChatEventGetDTO chatEventGetDTO, string[] botNames);
    }
}
