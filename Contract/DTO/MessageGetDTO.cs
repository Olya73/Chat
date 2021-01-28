using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class MessageGetDTO
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public UserGetDTO User { get; set; }
        public int UserId { get; set; }
        public int DialogId { get; set; }
    }
}
