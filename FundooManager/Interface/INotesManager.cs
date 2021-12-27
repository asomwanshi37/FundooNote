// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModel;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// INotesManager Interface
    /// </summary>
    public interface INotesManager
    {
        /// <summary>
        /// Adds notes to the table.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>returns a string when data added successful</returns>
        Task<NotesModel> AddNotes(NotesModel notesModel);

        /// <summary>
        /// Updates the title or note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string on successful update of data for title or Note</returns>
        Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel, int notesId);

        /// <summary>
        /// Updates the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>returns string on successful update of color</returns>
        Task<NotesModel> UpdateColor(int noteId, string color);

        /// <summary>
        /// Updates the archive.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns the string after updating archive</returns>
        Task<NotesModel> UpdateArchive(int notesId);

        /// <summary>
        /// Adds the pin.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a string after updating pin</returns>
        Task<NotesModel> AddPin(int notesId);

        /// <summary>
        /// Deletes the add to trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string on adding notes to trash after deletion</returns>
        Task<NotesModel> DeleteAddToTrash(int notesId);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns a retrieved notes</returns>
        Task<IEnumerable<NotesModel>> GetNotes(int userId);

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a string on successful restore</returns>
        Task<NotesModel> RestoreFromTrash(int notesId);

        /// <summary>
        /// Delete the note from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// returns a string on successful delete
        /// </returns>
        Task<NotesModel> DeleteNoteFromTrash(int notesId);

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="remainder">The reminder.</param>
        /// <returns>returns string on successful reminder set</returns>
        Task<NotesModel> SetReminder(int notesId, string remainder);

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string after removing the reminder</returns>
        Task<NotesModel> DeleteReminder(int notesId);
        /// <summary>
        /// Gets the remainder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns as result get reminder notes</returns>
        Task<IEnumerable<NotesModel>> GetReminderNotes(int userId);

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns as result get archive notes</returns>
        Task<IEnumerable<NotesModel>> GetArchiveNotes(int userId);

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns as result get trash notes</returns>
        Task<IEnumerable<NotesModel>> GetTrashNotes(int userId);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>returns string after successfully adding image</returns>
        Task<NotesModel> AddImage(int notesId, IFormFile image);

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string after successfully removing image</returns>
        Task<NotesModel> RemoveImage(int notesId);
    }
}
