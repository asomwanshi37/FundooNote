// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNote.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    /// <summary>
    /// class UserController 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
   
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager userManager;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The logger
        /// </summary>
        
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public UserController(IUserManager userManager, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers the specified user details.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns>Below function returns the status code as IActionResult</returns>
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userDetails)
        {
            try
            {
                this.logger.LogInformation(userDetails.FirstName + " " + userDetails.LastName + " is trying to register");
                RegisterModel result = await this.userManager.Register(userDetails);

                if (result != null)
                {
                    this.logger.LogInformation(userDetails.FirstName + " " + userDetails.LastName + " " + result);
                    return this.Ok(new ResponseModel<RegisterModel>() { Status = true, Message = "Registration Successfull !", Data = result });
                }
                else
                {
                    this.logger.LogInformation(userDetails.FirstName + " " + userDetails.LastName + " " + result);
                    return this.BadRequest(new { Status = false, Message = "Email Already Exists! Please Login" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occured while using register " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>Below function returns the status code as IActionResult</returns>
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
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(this.configuration["RedisServer"]);
                    IDatabase database = multiplexer.GetDatabase();
                    int userId = Convert.ToInt32(database.StringGet("userID"));
                    string firstName = database.StringGet("FirstName");
                    string lastName = database.StringGet("LastName");

                    return this.Ok(new { Status = true, Message = "successful Login", Data = result, Token = token });
                }
                else
                {
                    this.logger.LogInformation("Login Failed");
                    return this.BadRequest(new { Status = false, Message = "Enter Correct Email Details" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occured while using login " + ex.Message);
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns>Below function returns the status code as IActionResult</returns>
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
                    return this.Ok(new ResponseModel<ResetModel> { Status = true, Message = "Password Reset Successfully !" });
                }
                else
                {
                    this.logger.LogInformation("Password Reset Failed!");
                    return this.BadRequest(new { Status = false, Message = "Password Reset Unsuccessfully!. Invalid Email!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occured while using reset password " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Below function returns the status code as IActionResult</returns>
        [HttpPost]
        [Route("api/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                this.logger.LogInformation(email + "is using forgot password");
                var result = await this.userManager.ForgetPassword(email);
                if (result)
                {
                    this.logger.LogInformation(email);
                    return this.Ok(new { Status = true, Message = "Mail send Sucessful Please check Your EMail!" });
                }
                else
                {
                    this.logger.LogInformation(email);
                    return this.BadRequest(new { Status = false, Message = "Email not Exists Please Register" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception occured while using forgot password " + ex.Message);
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
