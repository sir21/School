using SchoolManagement.Data;
using SchoolManagement.Interfaces;
using SchoolManagement.Models.Entity;
using SchoolManagement.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Services
{
    public class AccountServices: IAccountService
    {
        private readonly StudentContext _context;

        public AccountServices(StudentContext context)
        {
            _context = context;
        }

        //Login function
        public async Task<LoginRespond> LoginServiceAsync (StudentLoginModel model)
        {
            //validation functions
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if(Validator.TryValidateObject(model, context, results, true))
            {
                string passwordEncoded = Encoder(model.Password);//encode password

                return await CheckInfoAsync(model, passwordEncoded);//check whether user exists
            }
            else
            {
                //If validation failed
                LoginRespond loginRespond = new LoginRespond
                {
                    Pass = false,
                    Email = model.Email,
                    Token = "Failed to login due to the incorrect email or password",
                    LoginTime = DateTime.Now
                };

                return loginRespond;
            }
        }

        private async Task<LoginRespond> CheckInfoAsync(StudentLoginModel studentLogin, string passwordEncoded)
        {
            //Create admin for initial development
            //User new_user = new User
            //{
            //    Email = "sas@mymail.mail",
            //    Password = Encoder("Pass@123"),
            //    UserRole = Role.Admin
            //};
            //_context.Users.Add(new_user);
            //await _context.SaveChangesAsync();

            var user = _context.Users.SingleOrDefault(i => i.Email == studentLogin.Email);
            if(user.Password == passwordEncoded)
            {
                LoginLogger loginLogger = new LoginLogger
                {
                    Email = studentLogin.Email,
                    Password = passwordEncoded,
                    LoginSuccess = true,
                    Time = DateTime.Now
                };
                _context.LoginLogs.Add(loginLogger);
                await _context.SaveChangesAsync();

                LoginRespond loginRespond = new LoginRespond
                {
                    Pass = true,
                    Email = loginLogger.Email,
                    Token = Encoder(DateTime.Now.ToString()),
                    LoginTime = DateTime.Now
                };

                return loginRespond;
            }
            else
            {
                LoginLogger loginLogger = new LoginLogger
                {
                    Email = studentLogin.Email,
                    Password = passwordEncoded,
                    LoginSuccess = false,
                    Time = DateTime.Now
                };
                _context.LoginLogs.Add(loginLogger);
                await _context.SaveChangesAsync();

                return new LoginRespond
                {
                    Pass = false,
                    Email = studentLogin.Email,
                    LoginTime = DateTime.Now,
                    Token = "Login Failed"
                };
            }
        }

        private string Encoder(string word)
        {
            byte[] encode = new byte[word.Length];
            encode = Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(encode);
        }
    }
}
