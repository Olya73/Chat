using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class ChatEventDTO
    {
        public int Id { get; set; }
        public string TypeOfAction { get; set; }
        public int DialogId { get; set; }
        public int UserId { get; set; }
        public long? MessageId { get; set; }
        public int State { get; set; }
    }
}
