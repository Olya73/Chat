using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Bot
{
    [Flags]
    public enum ActionTypes
    {
        NewMessage = 1,
        UserAdded = 2,
        UserDeleted = 4,
        NewCommand = 8,
        MessageDeleted = 16
    }
}
