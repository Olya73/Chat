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

        public string OnEvent(MessageGetDTO messageGetDTO, ActionTypes action)
        {
            if (action == AllowedActions)
            {
                if (!String.IsNullOrEmpty(messageGetDTO.User.Login))
                    return $"Hello {messageGetDTO.User.Login}";
            }
            return null;
        }
    }
}
