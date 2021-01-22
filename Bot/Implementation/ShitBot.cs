using BotLibrary.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class ShitBot : IEventBot, IMessageBot
    {
        public string Name => "Shit";
        public List<int> DialogIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string OnEvent(string v)
        {
            return Name;
        }

        public string OnMessage(string v)
        {
            throw new NotImplementedException();
        }
    }
}
