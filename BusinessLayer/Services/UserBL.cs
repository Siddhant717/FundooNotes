using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userRL.RegisterUser(userPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                return userRL.LoginUser(loginModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ForgotPassword(string emailid)
        {
            try
            {
                return userRL.ForgotPassword(emailid);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool ResetPassword(string emailid, PasswordModel passwordModel)
        {
            try
            {
                return this.userRL.ResetPassword(emailid, passwordModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}