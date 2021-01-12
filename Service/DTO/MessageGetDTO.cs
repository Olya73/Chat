using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class MessageGetDTO
    {
        public long Id { get; internal set; }
        public DateTime DateTime { get; internal set; }
        public string Text { get; set; }
    }
}
