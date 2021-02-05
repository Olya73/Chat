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

        public string OnEvent(ChatEventGetDTO chatEventGetDTO, ActionTypes action)
        {
            if (action == AllowedActions)
            {
                if (!String.IsNullOrEmpty(chatEventGetDTO.User.Login))
                    return $"Hello {chatEventGetDTO.User.Login}";
            }
            return null;
        }
    }
}
