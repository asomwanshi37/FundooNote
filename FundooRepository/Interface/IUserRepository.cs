// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using System.Threading.Tasks;
    using FundooModel;
    
    /// <summary>
    /// Interface UserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers the specified user details.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        /// <returns>Returns string if Register is successful</returns>
        Task<RegisterModel> Register(RegisterModel userDetails);
        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>
        /// Returns string if login is successful
        /// </returns>
        Task<RegisterModel> Login(LoginModel login);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>
        /// Returns true if the password is successfully reset
        /// </returns>
        Task<bool> ResetPassword(ResetModel resetPassword);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns True if mail sent successful else false
        /// </returns>
        Task<bool> ForgetPassword(string email);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns the token when user login
        /// </returns>
        string GenerateToken(string email);
    }
}
