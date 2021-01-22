using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Inteface
{
    public interface ICommandBot : IBot
    {
        string OnCommand(string v);
    }
}
