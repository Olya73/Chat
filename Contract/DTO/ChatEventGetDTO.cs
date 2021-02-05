using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class ChatEventGetDTO
    {
        public UserGetDTO User { get; set; }
        public MessageGetDTO Message { get; set; }
    }
}
