using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class TypeOfAction
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<ChatAction> ChatActions { get; set; }
    }
}
