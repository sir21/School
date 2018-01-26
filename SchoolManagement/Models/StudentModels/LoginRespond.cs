using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.StudentModels
{
    public class LoginRespond
    {
        public bool Pass { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
