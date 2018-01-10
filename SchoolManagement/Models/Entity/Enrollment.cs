using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.Entity
{
    public class Enrollment
    {
        public int EID { get; set; }
        public int SID { get; set; }
        public int CID { get; set; }
        public int? Marks { get; set; }
        public string Notes { get; set; }
    }
}
