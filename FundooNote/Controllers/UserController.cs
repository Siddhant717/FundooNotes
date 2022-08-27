using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        public UserController(IUserBL userBL, IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;

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
                this.userBL.LoginUser(loginModel);
                return this.Ok(new { success = true, status = 200, message = $"login successful for {loginModel.EmailId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}