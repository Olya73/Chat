using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Bot.Interface
{
    public interface IBotManager
    {
        IEnumerable<string> BotOnCommand(BotMessageDTO botMessage, string[] botNames);
        IEnumerable<string> BotOnEvent(string[] botNames, ActionTypes actionType, BotMessageDTO botMessage);
        IEnumerable<string> BotOnMessage(BotMessageDTO botMessage, string[] botNames);
    }
}
