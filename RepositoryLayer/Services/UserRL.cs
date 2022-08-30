using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly FundooContext fundooContext;
        private IConfiguration _config;
        public UserRL(FundooContext fundooContext, IConfiguration Config)
        {
            this.fundooContext = fundooContext;
            this._config = Config;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.EmailId == loginModel.EmailId && x.Password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                return GenerateJwtToken(user.EmailId, user.userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJwtToken(string emailId, int userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", emailId),
                    new Claim("UserId",userId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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
                fundooContext.Users.Add(user);
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgotPassword(string emailid)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.EmailId == emailid).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                MessageQueue fundooQ = new MessageQueue();

                //Setting the QueuPath where we want to store the messages.
                fundooQ.Path = @".\private$\FundooNote";
                if (MessageQueue.Exists(fundooQ.Path))
                {

                    fundooQ = new MessageQueue(@".\private$\FundooNote");
                    //Exists
                }
                else
                {
                    // Creates the new queue named "Bills"
                    MessageQueue.Create(fundooQ.Path);
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJwtToken(emailid, user.userId);
                MyMessage.Label = "Forget Password Email";
                fundooQ.Send(MyMessage);
                Message msg = fundooQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(emailid, msg.Body.ToString(),user.FirstName);
                fundooQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                fundooQ.BeginReceive();
                fundooQ.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)

            {

                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)

                {

                    Console.WriteLine("Access is denied. " +

                        "Queue might be a system queue.");

                }

            }
        }

        private string GenerateToken(string emailid)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", emailid),

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                     new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}