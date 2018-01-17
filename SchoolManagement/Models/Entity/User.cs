using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.StudentModels
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
    }

    public enum Role
    {
        Admin,
        Student
    }
}
