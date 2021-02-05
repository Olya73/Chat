using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class UserBotMessageDTO
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}
