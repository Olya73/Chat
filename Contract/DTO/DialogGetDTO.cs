using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.DTO
{
    public class DialogGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTeteATete { get; set; }
        public List<int> UserIds { get; set; }
    }
}
