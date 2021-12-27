// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Context
{
    using FundooModel;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class UserContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public DbSet<RegisterModel> User { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public DbSet<NotesModel> Notes { get; set; }

        /// <summary>
        /// Gets or sets the collaborator.
        /// </summary>
        /// <value>
        /// The collaborator.
        /// </value>
        public DbSet<CollaboratorModel> Collaborator { get; set; }
    }
}
