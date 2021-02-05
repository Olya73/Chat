using Contract.Bot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class ChatEventAddDTO
    {
        public ActionTypes ActionType { get; set; }
        public int DialogId { get; set; }
        public int UserId { get; set; }
        public long? MessageId { get; set; }
    }
}
