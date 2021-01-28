using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class HiBot : IEventBot
    {
        public ActionTypes AllowedActions => ActionTypes.UserAdded;
        public string Name => "Hi";

        public string OnEvent(BotMessageDTO botMessageDTO, ActionTypes action)
        {
            if (action == AllowedActions)
            {
                if (String.IsNullOrEmpty(botMessageDTO.Login))
                    return $"Hello {botMessageDTO.Login}";
            }
            return null;
        }
    }
}
