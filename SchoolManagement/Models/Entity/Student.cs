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
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        [StringLength(40)]
        public string Guardian { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public bool AdminPermition { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
    }
}
