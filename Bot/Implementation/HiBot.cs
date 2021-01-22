using BotLibrary.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class HiBot : IEventBot
    {
        public string Name => "Hi";
        public List<int> DialogIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string OnEvent(string v)
        {
            return Name;
        }
    }
}
