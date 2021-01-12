﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class User
    {
        public int Id { get; set; }
        public int Login { get; set; }

        public List<UserDialog> UserDialogs { get; set; }
    }
}
