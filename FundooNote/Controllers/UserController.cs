using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        public IConfiguration configuration { get; }
        private readonly ILogger<UserController> logger;
        public UserController(IUserManager userManager, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel UserDetails)
        {
            try
            {
                this.logger.LogInformation(UserDetails.FirstName + " " + UserDetails.LastName + " is trying to register");
                 RegisterModel result = await this.userManager.Register(UserDetails);

                if (result != null)
                {
                    this.logger.LogInformation(UserDetails.FirstName + " " + UserDetails.LastName + " " + result);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration Successfull !"});
                }
                else
                {
                    this.logger.LogInformation(UserDetails.FirstName + " " + UserDetails.LastName + " " + result);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Email Already Exists! Please Login" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using register " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                this.logger.LogInformation(loginModel.Email + " is trying to Login");
                RegisterModel result = await this.userManager.Login(loginModel);
                string token = this.userManager.GenerateToken(loginModel.Email);

                if (result != null)
                {
                    this.logger.LogInformation(loginModel.Email + " logged in successfully and the token generated is " + token);
                    HttpContext.Session.SetString("UserEmail", loginModel.Email + " " +loginModel.Password);
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    int UserId = Convert.ToInt32(database.StringGet("UserID"));
                    string firstName = database.StringGet("FirstName");
                    string lastName = database.StringGet("LastName");
                    
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "successful Login", Data =result + "Token:" + token });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Enter Correct Email Details" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string> { Status = true, Message = ex.Message });
            }
        }


        [HttpPut]
        [Route("api/ResetPassword")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetModel resetData)
        {
            try
            {
                this.logger.LogInformation(resetData.Email + "is using reset password");
                var result = await this.userManager.ResetPassword(resetData);
                if (result == true)
                {
                    this.logger.LogInformation("Password reseted Successfully for " + resetData.Email);
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Password Reset Successfully !"});
                }
                else
                {
                    this.logger.LogInformation("Password Reset Failed!");
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Password Reset Unsuccessfully!. Invalid Email!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using reset password " + ex.Message);
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string Email)
        {
            try
            {
                this.logger.LogInformation(Email + "is using forgot password");
                var result = await this.userManager.ForgetPassword(Email);
                if (result)
                {
                    this.logger.LogInformation(Email);
                    return this.Ok(new ResponseModel<string> { Status = true, Message = "Mail send Sucessful Please check Your EMail!"});
                }
                else
                {
                    this.logger.LogInformation(Email);
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Email not Exists Please Register" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using forgot password " + ex.Message);
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
