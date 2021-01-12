using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class DialogAddDTO
    {
        public string Title { get; set; }
        public bool IsTeteATete { get; set; }
        public List<int> UsersId { get; set; }
    }
}
