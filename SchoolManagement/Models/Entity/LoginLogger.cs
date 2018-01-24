using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.Entity
{
    public class LoginLogger
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool LoginSuccess { get; set; }
        public DateTime Time { get; set; }
    }
}
