using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class ChatEvent
    {
        public int Id { get; set; }
        public int TypeOfActionId { get; set; }
        public int DialogId { get; set; }
        public int UserId { get; set; }
        public long? MessageId { get; set; }
        public int State { get; set; }

        public List<BotActionOnEvent> BotActionOnEvents { get; set; }
        public TypeOfAction TypeOfAction { get; set; }
        public Dialog Dialog { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
    }
}
