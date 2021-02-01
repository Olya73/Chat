using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Dialog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTeteATete { get; set; }

        public List<BotDialog> BotDialogs { get; set; }
        public List<UserDialog> UserDialogs { get; set; }
        public List<ChatEvent> ChatEvents { get; set; }

    }
}
