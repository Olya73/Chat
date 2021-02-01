using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }

        //public List<Message> Messages { get; set; }
        public List<UserDialog> UserDialogs { get; set; }
        public List<ChatEvent> ChatEvents{ get; set; }
    }
}
