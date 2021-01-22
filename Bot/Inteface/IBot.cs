using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Inteface
{
    public interface IBot
    {
        string Name { get;}
        List<int> DialogIds { get; set; }
    }
}
