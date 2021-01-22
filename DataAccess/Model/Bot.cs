using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Bot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Implementation { get; set; }

        public List<BotTypeOfBot> BotTypes { get; set; }
        public List<BotDialog> BotDialogs { get; set; }
        public List<ChatAction> ChatActions { get; set; }

    }
}
