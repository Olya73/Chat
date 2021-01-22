using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class TypeOfBot
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Inteface { get; set; }

        public List<BotTypeOfBot> BotTypes { get; set; }
    }
}
