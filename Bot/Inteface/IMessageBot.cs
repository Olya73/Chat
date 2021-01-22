using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Inteface
{
    public interface IMessageBot : IBot
    {
        string OnMessage(string v);
    }
}
