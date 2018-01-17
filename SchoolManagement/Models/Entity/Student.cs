using SchoolManagement.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class Student
    {
        [Key]
        public int SID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Guardian { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
    }
}
