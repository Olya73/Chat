using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class MessageAddDTO
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int DialogId { get; set; }
        public int UserId { get; set; }
    }
}
