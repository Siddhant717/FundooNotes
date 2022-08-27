using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly FundooContext funDoNoteContext;
        private IConfiguration _config;
        public UserRL(FundooContext funDoNoteContext)
        {
            this.funDoNoteContext = funDoNoteContext;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = funDoNoteContext.Users.Where(x => x.EmailId == loginModel.EmailId && x.Password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                return GenerateJwtToken(user.EmailId, user.Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJwtToken(string emailId, string password)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.EmailId = userPostModel.EmailId;
                user.Password = userPostModel.Password;
                user.Password = userPostModel.Password;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                funDoNoteContext.Users.Add(user);
                funDoNoteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}