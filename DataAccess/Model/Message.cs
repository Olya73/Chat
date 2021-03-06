﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Message
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        
        public int UserDialogId { get; set; }
        public UserDialog UserDialog { get; set; }
        public List<ChatEvent> ChatEvents{ get; set; }
    }
}
