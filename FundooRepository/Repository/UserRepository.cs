// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooModel;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;
   
    /// <summary>
    /// User Repository performs action with database,send email operation
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers the specified user details.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns>Returns true if Register is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<RegisterModel> Register(RegisterModel userDetails)
        {
            try
            {
                var exist = await this.userContext.User.Where(x => x.Email == userDetails.Email).SingleOrDefaultAsync();
                if (exist == null)
                {
                    userDetails.Password = this.PasswordEncryption(userDetails.Password);
                    this.userContext.User.Add(userDetails);
                    await this.userContext.SaveChangesAsync();
                    return userDetails;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>Returns true if Login is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<RegisterModel> Login(LoginModel loginData)
        {
            try
            {
                var exist = await this.userContext.User.Where(x => x.Email == loginData.Email).SingleOrDefaultAsync();
                if (exist != null)
                {
                    exist.Password = this.PasswordEncryption(exist.Password);
                    var details = await this.userContext.User.Where(x => x.Email == loginData.Email && x.Password == loginData.Password).SingleOrDefaultAsync();
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    database.StringSet(key: "UserID", exist.UserId.ToString());
                    database.StringSet(key: "Email", exist.Email);
                    database.StringSet(key: "FirstName", exist.FirstName);
                    database.StringSet(key: "LastName", exist.LastName);
                    return exist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>Returns true if the password is successfully reset</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<bool> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                var userPassword = await this.userContext.User.Where(x => x.Email == resetPassword.Email).SingleOrDefaultAsync();
                if (userPassword != null)
                {
                    resetPassword.Password = this.PasswordEncryption(resetPassword.Password);

                    this.userContext.Update(userPassword);

                    await this.userContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns true if mail sent successfully else false</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                var exist = await this.userContext.User.Where(x => x.Email == email).SingleOrDefaultAsync();
                if (exist != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(email);
                    this.SendMSMQ();
                    mail.Body = this.ReceieveMSMQ();

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(mail);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends the MSMQ =>microsoft message queue.
        /// </summary>
        /// <return>
        /// Returns true if the message in the queue is sent successfully
        /// </return>
        public void SendMSMQ()
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\Fundoo"))
            {
                msgqueue = new MessageQueue(@".\Private$\Fundoo");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\Fundoo");
            }

            msgqueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            string body = "This is for testing SMTP mail from Gmail";
            msgqueue.Label = "Mail Body";
            msgqueue.Send(body);
        }

        /// <summary>
        /// Receieve the MSMQ=>microsoft message queue.
        /// </summary>
        /// <returns>
        /// Returns true if the message in the queue is sent successfully
        /// </returns>
        public string ReceieveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var receievemsg = msgqueue.Receive();
            receievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receievemsg.ToString();
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns the token when user logins</returns>
        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                     new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Passwords the encryption.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>encoded password</returns>
        /// <exception cref="System.Exception">
        /// Error in base64Encode" + ex.Message
        /// </exception>
        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
