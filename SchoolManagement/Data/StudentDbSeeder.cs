using SchoolManagement.Models.StudentModels;
using SchoolManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Data
{
    public class StudentDbSeeder
    {
        private readonly StudentContext _context;

        public StudentDbSeeder(StudentContext context)
        {
            _context = context;
        }

        public async void Seed()
        {
            if (!_context.Users.Any())
            {
                //Create admin for initial development

                User new_user = new User
                {
                    Email = "sas@mymail.mail",
                    Password = "UGFzc0AxMjM=",
                    UserRole = Role.Admin
                };

                _context.Users.Add(new_user);
                await _context.SaveChangesAsync();
            }

            if (_context.TokenLogs.Any())
            {
                _context.TokenLogs.RemoveRange(_context.TokenLogs);
                await _context.SaveChangesAsync();
            }
        }
    }
}
