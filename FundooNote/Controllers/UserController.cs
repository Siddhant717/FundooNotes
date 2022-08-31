using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        private FundooContext fundooContext;
        public UserController(IUserBL userBL, IConfiguration config, FundooContext fundooContext)
        {
            this.userBL = userBL;
            this._config = config;
            this.fundooContext = fundooContext;

        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { sucess = true, status = 200, message = $"Registration sucessful for {userPostModel.EmailId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {loginModel.EmailId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string emailid)
        {
            try
            {
                bool isExist = this.userBL.ForgotPassword(emailid);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to Email : {emailid}" });
                else return BadRequest(new { success = false, message = $"No user Exist with Email : {emailid}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel passwordModel)
        {
            try
            {
                if (passwordModel.NewPassword != passwordModel.ConfirmNewPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                //Authorization, match email from token
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result = fundooContext.Users.Where(u => u.userId == UserID).FirstOrDefault();
                string Email = result.EmailId.ToString();
                bool res = this.userBL.ResetPassword(Email, passwordModel);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = $"Password not updated" });
                }
                return this.Ok(new { success = true, status = 200, message = "Password Changed Sucessfully" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}