using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class BotActionOnEvent
    {
        public int Id { get; set; }
        public int ChatEventId { get; set; }
        public string BotName { get; set; }
        public string BotResponse { get; set; }

        public ChatEvent ChatEvent { get; set; }
        public Bot Bot { get; set; }
    }
}
