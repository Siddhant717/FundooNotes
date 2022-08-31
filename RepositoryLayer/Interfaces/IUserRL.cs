using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        void RegisterUser(UserPostModel userPostModel);
        public string LoginUser(LoginModel loginModel);
        public bool ForgotPassword(string emailid);
        public bool ResetPassword(string emailid, PasswordModel passwordModel);
    }
}
