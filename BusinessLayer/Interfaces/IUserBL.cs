using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        void RegisterUser(UserPostModel userPostModel);
        public string LoginUser(LoginModel loginModel);
        public bool ForgotPassword(string emailid);
        public bool ResetPassword(string email, PasswordModel passwordModel);

    }
}
