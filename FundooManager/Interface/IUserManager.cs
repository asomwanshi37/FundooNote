using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
   public interface IUserManager
    {
        Task<RegisterModel> Register(RegisterModel UserDetails);
        Task<RegisterModel> Login(LoginModel login);
        Task<bool> ResetPassword(ResetModel resetPassword);
        Task<bool> ForgetPassword(string Email);
        string GenerateToken(string Email);
    }
}
