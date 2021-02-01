using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Bot.Interface
{
    public interface IBotManager
    {
        IEnumerable<BotMessageDTO> BotOnCommand(MessageGetDTO messageGetDTO, string[] botNames);
        IEnumerable<BotMessageDTO> BotOnEvent(MessageGetDTO messageGetDTO, string[] botNames, ActionTypes actionType);
        IEnumerable<BotMessageDTO> BotOnMessage(MessageGetDTO messageGetDTO, string[] botNames);
    }
}
