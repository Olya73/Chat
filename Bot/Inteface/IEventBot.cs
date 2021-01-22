using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Inteface
{
    public interface IEventBot : IBot
    {
        string OnEvent(string v);
    }
}
