using SchoolManagement.Models.StudentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Interfaces
{
    public interface IAccountService
    {
        Task<LoginRespond> LoginServiceAsync(StudentLoginModel model);
        bool Authenticate(string token, string email);
        Task<bool> LogoutService(string email);
        Task<bool> RegistrationService(StudentRegisterModel registerModel);
    }
}
