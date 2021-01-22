using BotLibrary.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class WeatherBot : ICommandBot
    {
        public string Name => "Weather";

        public List<int> DialogIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string OnCommand(string v)
        {
            return Name;
        }
    }
}
