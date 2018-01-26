using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.StudentModels
{
    public class TokenLog
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
