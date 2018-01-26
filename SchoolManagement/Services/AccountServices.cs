using SchoolManagement.Data;
using SchoolManagement.Interfaces;
using SchoolManagement.Models;
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

        //For login --helper
        //Generate token and update logs
        private async Task<LoginRespond> CheckInfoAsync(StudentLoginModel studentLogin, string passwordEncoded)
        {
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
                string token = Encoder(DateTime.Now.ToString());

                var student = _context.Students.SingleOrDefault(i => i.Email == studentLogin.Email);
                bool isAccepted = false;
                if (student != null)
                {
                    isAccepted = student.AdminPermition == true ? true : false;
                }

                LoginRespond loginRespond = new LoginRespond
                {
                    Pass = true,
                    Email = loginLogger.Email,
                    Token = token,
                    LoginTime = DateTime.Now,
                    IsAdmin = user.UserRole == Role.Admin ? true : false,
                    IsAccepted = isAccepted
                };

                await AddTokenLog(loginLogger.Email, token, user.UserRole);

                user = null;

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

        //genarate encryptions
        private string Encoder(string word)
        {
            byte[] encode = new byte[word.Length];
            encode = Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(encode);
        }

        //updated token
        private async Task AddTokenLog(string email, string token, Role role)
        {
            var tokenRecord = _context.TokenLogs.SingleOrDefault(i => i.Email == email);
            if (tokenRecord != null)
            {
                tokenRecord.Token = token;
                _context.TokenLogs.Update(tokenRecord);
                await _context.SaveChangesAsync();
            }
            TokenLog tokenLog = new TokenLog
            {
                Email = email,
                Token = token,
                IsAdmin = role == Role.Admin ? true : false 
            };

            _context.Add(tokenLog);
            await _context.SaveChangesAsync();
        }

        //Authenticate requests from clients using token and email
        public bool Authenticate(string email, string token)
        {
            var tokenDB = _context.TokenLogs.SingleOrDefault(i => i.Email == email);
            if (tokenDB.Token == token)
                return true;
            return false;
        }

        //check whether request is comming from admin user
        public bool IsAdminAuthenticate(string email, string token)
        {
            var tokenDB = _context.TokenLogs.SingleOrDefault(i => i.Email == email);
            if (tokenDB == null)
                return false;
            if (tokenDB.Token == token && tokenDB.IsAdmin)
                return true;
            return false;
        }

        //logout function
        public async Task<bool> LogoutService(string email)
        {
            try
            {
                var removingToken = _context.TokenLogs.SingleOrDefault(i => i.Email == email);
                _context.TokenLogs.Remove(removingToken);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        //registration service funtion
        public async Task<bool> RegistrationService(StudentRegisterModel registerModel)
        {
            try
            {
                //validation functions
                var context = new ValidationContext(registerModel, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(registerModel, context, results, true))
                {
                    if (CheckEmailAvailability(registerModel.Email)){
                        if (registerModel.Password != registerModel.ConfirmPassword)
                            return false;
                        Student student = new Student
                        {
                            FirstName = registerModel.FirstName,
                            LastName = registerModel.LastName,
                            Email = registerModel.Email,
                            Guardian = registerModel.Guardian,
                            Address = new Address
                            {
                                Number = registerModel.Address1,
                                Street = registerModel.Address2,
                                Town = registerModel.Address3
                            },
                            DateOfBirth = registerModel.DateOfBirth,
                            AdminPermition = false
                        };
                        User user = new User
                        {
                            Email = registerModel.Email,
                            Password = Encoder(registerModel.Password),
                            UserRole = Role.Student
                        };

                        _context.Add(student);
                        _context.Add(user);

                        await _context.SaveChangesAsync();

                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //check if any accounts are available for provided email
        private bool CheckEmailAvailability(string email)
        {
            var user = _context.Users.SingleOrDefault(i => i.Email == email);
            if (user == null)
                return true;
            return false;
        }
    }
}
