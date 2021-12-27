// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// CollaboratorModel Class
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the col identifier.
        /// </summary>
        /// <value>
        /// The col identifier.
        /// </value>
        [Key]
        public int ColId { get; set; }

        /// <summary>
        /// Gets or sets the col email.
        /// </summary>
        /// <value>
        /// The col email.
        /// </value>
        public string ColEmail { get; set; }

        /// <summary>
        /// Gets or sets ForeginKey.
        /// </summary>
        [ForeignKey("NotesModel")]

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The col identifier.
        /// </value>
        public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the notes model.
        /// </summary>
        /// <value>
        /// The notes model.
        /// </value>
        public NotesModel NotesModel { get; set; }
    }
}
