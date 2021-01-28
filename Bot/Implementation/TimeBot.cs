﻿using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BotLibrary.Implementation
{
    class TimeBot : IMessageBot, ICommandBot
    {
        public string Name => "Time";
        public readonly string[] commandList = { 
            "/now",
            "/utc"
        };
        public string[] CommandList => commandList;
        string pattern = @"^через\s([0-5]?\d|60)\z";
        string onCommandResponse = "Через {0} минут будет {1}:{2}.";

        public string OnCommand(BotMessageDTO botMessageDTO)
        {
            int existing = CommandExists(botMessageDTO.Message);
            switch (existing)
            { 
                case 0 :
                    return DateTime.Now.ToString();
                case 1 :
                    return DateTime.UtcNow.ToString();
                default:
                    return null;
            }
        }

        public string OnMessage(BotMessageDTO botMessageDTO)
        {
            Match match = Regex.Match(botMessageDTO.Message, pattern);
            if (match.Success)
            {
                int minutes = int.Parse(match.Result("$1"));
                var date = DateTime.Now.AddMinutes(minutes);
                TimeSpan time = date.TimeOfDay;
                return String.Format(onCommandResponse, minutes, time.Hours, time.Minutes);                
            }
            return null;
        }

        public int CommandExists(string comm)
        {
            return Array.FindIndex(commandList, c => c.Equals(comm, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
