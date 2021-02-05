using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    [NotMapped]
    public class UserBotMessage
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}
