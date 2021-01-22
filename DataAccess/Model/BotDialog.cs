using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class BotDialog
    {
        public string BotName { get; set; }
        public int DialogId { get; set; }

        public Bot Bot { get; set; }
        public Dialog Dialog { get; set; }
    }
}
