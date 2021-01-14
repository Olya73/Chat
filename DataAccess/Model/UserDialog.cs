using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class UserDialog
    {
        public int Id { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public int UserId { get; set; }
        public int DialogId { get; set; }

        public User User { get; set; }
        public Dialog Dialog { get; set; }
        public List<Message> Messages { get; set; }
    }
}
