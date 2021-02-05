using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BotLibrary.Implementation
{
    class ShitBot : IEventBot, IMessageBot
    {
        public string Name => "Shit";
        public ActionTypes AllowedActions => ActionTypes.UserAdded | ActionTypes.UserDeleted | ActionTypes.NewMessage;
        private string[] _answersOnDelete = 
        {   "Проваливай!", 
            "Отдыхай!",
            "Anu belore dala'na" };
        private string[] _answersOnMessage =
        {   "",
            "",
            ""
        };
        private string[] _replacements =
        {
            "блин",
            "капец",
            "кароч"
        };

        public string OnEvent(ChatEventGetDTO chatEventGetDTO, ActionTypes action)
        {
            if ((AllowedActions & action) == ActionTypes.UserAdded)
            {
                return $"Ты кто? Кто его позвал? Чей это друг?";
            }
            else if ((AllowedActions & action) == ActionTypes.UserDeleted)
            {
                return _answersOnDelete[new Random().Next(_answersOnDelete.Length)];
            }
            else if ((AllowedActions & action) == ActionTypes.NewMessage)
            {
                return _answersOnMessage[new Random().Next(_answersOnMessage.Length)];
            }
            return null;
        }

        public string OnMessage(ChatEventGetDTO chatEventGetDTO)
        {
            string pattern = @"((\w+\s\w+){" + new Random().Next(1, 4) + @"})\s";
            string replacement = "$1 " + _replacements[new Random().Next(_replacements.Length)] + " ";

            return Regex.Replace(chatEventGetDTO.Message.Text, pattern, replacement);
        }
    }
}
