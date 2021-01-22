using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Inteface
{
    public interface IBotManager
    {
        IEnumerable<string> BotOnMessage(string[] botsNames, string actionType);
    }
}
