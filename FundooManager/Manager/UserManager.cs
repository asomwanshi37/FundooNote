// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;
    
    /// <summary>
    ///  UserManager Class manages the operation done by the user
    /// </summary>
    /// <seealso cref="FundooManager.Interface.IUserManager" />
    public class UserManager : IUserManager
    {
        /// <summary>
        /// IUserRepository type object
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class and create object for UserManager on run time.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user details.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns>Returns string if Register is successful </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<RegisterModel> Register(RegisterModel userDetails)
        {
            try
            {
                return await this.repository.Register(userDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Logins the specified login model.
        /// </summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>Returns string if Login is successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<RegisterModel> Login(LoginModel loginModel)
        {
            try
            {
                return await this.repository.Login(loginModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns> Returns true if the password is successfully reset </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<bool> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                return await this.repository.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns True if mail sent successful else false</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<bool> ForgetPassword(string email)
        {
            try
            {
                return await this.repository.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns the token when user login</returns>
        /// <exception cref="System.Exception"></exception>
        public string GenerateToken(string email)
        {
            try
            {
                return this.repository.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
