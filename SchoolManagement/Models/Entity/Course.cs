using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.Entity
{
    public class Course
    {
        [Key]
        public int CID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
