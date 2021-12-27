// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// class NotesManager
    /// </summary>
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// The notes repository
        /// </summary>
        private readonly INotesRepository notesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager"/> class and create a object of INotesRepository on run time.
        /// </summary>
        /// <param name="notesRepository">
        /// The notes repository.
        /// </param>
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        /// <summary>
        /// Adds notes to the table.
        /// </summary>
        /// <param name="notesModel">
        /// The notes model.
        /// </param>
        /// <returns>returns a string when data added successful</returns>
        public async Task<NotesModel> AddNotes(NotesModel notesModel)
        {
            try
            {
                return await this.notesRepository.AddNotes(notesModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the title or note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string on successful update of data for title or Note</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel, int notesId)
        {
            try
            {
                return await this.notesRepository.UpdateTitleOrNote(notesModel, notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>returns string on successful update of Color</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateColor(int noteId, string color)
        {
            try
            {
                return await this.notesRepository.UpdateColor(noteId, color);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the archive.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns the string after updating archive</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateArchive(int notesId)
        {
            try
            {
                return await this.notesRepository.UpdateArchive(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the pin.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a string after updating pin</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> AddPin(int notesId)
        {
            try
            {
                return await this.notesRepository.AddPin(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the add to trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string on adding notes to trash after deletion</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteAddToTrash(int notesId)
        {
            try
            {
                return await this.notesRepository.DeleteAddToTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Returns a retrieved notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a string on successful restore</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> RestoreFromTrash(int notesId)
        {
            try
            {
                return await this.notesRepository.RestoreFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete the note from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns a string on successful delete</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteNoteFromTrash(int notesId)
        {
            try
            {
                return await this.notesRepository.DeleteNoteFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="remainder">The reminder.</param>
        /// <returns>returns string on successful reminder set</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> SetReminder(int notesId, string reminder)
        {
            try
            {
                return await this.notesRepository.SetReminder(notesId, reminder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string after removing the reminder</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteReminder(int notesId)
        {
            try
            {
                return await this.notesRepository.DeleteReminder(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns ienumerable as result</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetReminderNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetReminderNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns ienumerable as result</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetArchiveNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetArchiveNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns ienumerable as result</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetTrashNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetTrashNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>returns string after successfully adding image</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> AddImage(int notesId, IFormFile image)
        {
            try
            {
                return await this.notesRepository.AddImage(notesId, image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns string after successfully removing image</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> RemoveImage(int notesId)
        {
            try
            {
                return await this.notesRepository.RemoveImage(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
