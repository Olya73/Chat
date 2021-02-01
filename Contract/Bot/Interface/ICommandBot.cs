using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Bot.Interface
{
    public interface ICommandBot : IBot
    {
        string OnCommand(MessageGetDTO messageGetDTO);
        int CommandExists(string comm);
    }
}
