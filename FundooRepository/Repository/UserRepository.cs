using Experimental.System.Messaging;
using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IDatabase = StackExchange.Redis.IDatabase;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        private readonly IConfiguration configuration;

        public UserRepository(UserContext userContext,IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        public async Task<RegisterModel> Register(RegisterModel UserDetails)
        {
            try
            {
                var exist = await this.userContext.User.Where(x => x.Email == UserDetails.Email).SingleOrDefaultAsync();
                if (exist == null)
                {
                    UserDetails.Password = PasswordEncryption(UserDetails.Password);
                        this.userContext.User.Add(UserDetails);
                    await this.userContext.SaveChangesAsync();
                        return UserDetails;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RegisterModel> Login(LoginModel loginData)
        {
            try
            {
                var exist = await this.userContext.User.Where(x => x.Email == loginData.Email).SingleOrDefaultAsync();
                if (exist != null)
                {
                    exist.Password = PasswordEncryption(exist.Password);
                    var Details = await  this.userContext.User.Where(x => x.Email == loginData.Email && x.Password == loginData.Password).SingleOrDefaultAsync();
                    ConnectionMultiplexer Multiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServer"]);
                    IDatabase database = Multiplexer.GetDatabase();
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
        public async Task<bool> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                var userPassword = await this.userContext.User.Where(x => x.Email == resetPassword.Email).SingleOrDefaultAsync();
                if (userPassword != null)
                {
                    resetPassword.Password = PasswordEncryption(resetPassword.Password);

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
        public async Task<bool> ForgetPassword(string Email)
        {
            try
            {

                var Exist = await this.userContext.User.Where(x => x.Email == Email).SingleOrDefaultAsync();
                if (Exist != null)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(this.configuration["Credentials:Email"]);
                    mail.To.Add(Email);
                    SendMSMQ();
                    mail.Body = ReceieveMSMQ();

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:Email"], this.configuration["Credentials:Password"]);
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
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

        public string ReceieveMSMQ()
        {
            MessageQueue msgqueue = new MessageQueue(@".\Private$\Fundoo");
            var receievemsg = msgqueue.Receive();
            receievemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receievemsg.ToString();
        }
        public string GenerateToken(string Email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Name, Email)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
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
