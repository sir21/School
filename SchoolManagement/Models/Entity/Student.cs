using SchoolManagement.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class Student
    {
        public int SID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Guardian { get; set; }
        public DateTime Date { get; set; }
    }
}
