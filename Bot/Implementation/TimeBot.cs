using BotLibrary.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class TimeBot : IMessageBot, ICommandBot
    {
        public string Name => "Time";

        public List<int> DialogIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string OnCommand(string v)
        {
            throw new NotImplementedException();
        }

        public string OnMessage(string v)
        {
            throw new NotImplementedException();
        }
    }
}
