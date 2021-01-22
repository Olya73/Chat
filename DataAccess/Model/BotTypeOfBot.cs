using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class BotTypeOfBot
    {
        public string BotName { get; set; }
        public int TypeOfBotId { get; set; }
        public string[] Members { get; set; }

        public Bot Bot { get; set; }
        public TypeOfBot TypeOfBot { get; set; }
    }
}
