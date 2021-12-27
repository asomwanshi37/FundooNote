// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// class NotesModel
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [Key]
        public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public string Reminder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        public bool Is_Archive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        public bool Is_Trash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pin; otherwise, <c>false</c>.
        /// </value>
        public bool Is_Pin { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("RegisterModel")]
        
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the register model.
        /// </summary>
        /// <value>
        /// The register model.
        /// </value>
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
