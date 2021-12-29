// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModel;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// class NotesRepository 
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.INotesRepository" />
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="configuration">The configuration.</param>
        public NotesRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return a string when data added successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> AddNotes(NotesModel notesModel)
        {
            try
            {
                if (notesModel.Title != null && notesModel.Notes != null && notesModel.Color != null && notesModel.Reminder != null)
                {
                    await this.userContext.Notes.AddAsync(notesModel);
                    await this.userContext.SaveChangesAsync();
                    return notesModel;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the title or note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return string on successful update of data for title or Note</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel, int notesId)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NotesId == notesModel.NotesId).SingleOrDefault();
                if (exists != null)
                {
                    exists.Notes = notesModel.Notes;
                    exists.Title = notesModel.Title;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>returns string on successful update of color </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateColor(int noteId, string color)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NotesId == noteId).SingleOrDefault();
                if (exists != null && color != null)
                {
                    exists.Color = color;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the archive.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return the string after updating archive</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> UpdateArchive(int notesId)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefault();
                string message = string.Empty;
                if (exists != null)
                {
                    if (exists.Is_Archive == true)
                    {
                        exists.Is_Archive = false;
                        message = "Note unarchived";
                    }
                    else
                    {
                        exists.Is_Archive = true;
                        message = "Note archived";
                        if (exists.Is_Pin == true)
                        {
                            exists.Is_Pin = false;
                            message = "Note unpinned and archived";
                        }
                    }

                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the pin.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a string after updating pin </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> AddPin(int notesId)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefault();
                string message = string.Empty;
                if (exists != null)
                {
                    if (exists.Is_Pin == true)
                    {
                        exists.Is_Pin = false;
                        message = "Unpin Note";
                    }
                    else
                    {
                        exists.Is_Pin = true;
                        message = "Pinned";
                        if (exists.Is_Archive == true)
                        {
                            exists.Is_Archive = false;
                            message = "Note unarchived and pinned";
                        }
                    }

                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the add to trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return string on adding notes to trash after deletion</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteAddToTrash(int notesId)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefault();
                string message = string.Empty;
                if (exists != null)
                {
                    exists.Is_Trash = true;
                    message = "Note trashed";

                    if (exists.Is_Pin == true)
                    {
                        exists.Is_Pin = false;
                        message = "Note unpinned and trashed";
                    }

                    exists.Reminder = null;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                }
                else
                {
                    message = "Note Not present! Add Note";
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Return a retrieved notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetNotes(int userId)
        {
            try
            {
                var noteList = await this.userContext.Notes.Where(x => x.UserId == userId).ToListAsync();
                if (noteList != null)
                {
                    return noteList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns> return a string on successful restore</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> RestoreFromTrash(int notesId)
        {
            try
            {
                var exists = await this.userContext.Notes.Where(x => x.NotesId == notesId && x.Is_Trash == true).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Is_Trash = false;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete the note from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a string on successful delete</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteNoteFromTrash(int notesId)
        {
            try
            {
                var exists = await this.userContext.Notes.Where(x => x.NotesId == notesId && x.Is_Trash == true).SingleOrDefaultAsync();
                if (exists != null)
                {
                    this.userContext.Notes.Remove(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sets the remainder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns> return string on successful reminder set</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> SetReminder(int notesId, string reminder)
        {
            try
            {
                var exists = await this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Reminder = reminder;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// return string on successful reminder delete
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> DeleteReminder(int notesId)
        {
            try
            {
                var exists = await this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Reminder = null;
                    this.userContext.Notes.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the remainder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return a successful get remainder notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetReminderNotes(int userId)
        {
            try
            {
                var noteList = await this.userContext.Notes.Where(x => x.UserId == userId).ToListAsync();
                if (noteList.Count() != 0)
                {
                    return noteList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return a successful get archives notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetArchiveNotes(int userId)
        {
            try
            {
                var noteList = await this.userContext.Notes.Where(x => x.UserId == userId).ToListAsync();
                if (noteList.Count() != 0)
                {
                    return noteList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// return a successful get trash notes
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<NotesModel>> GetTrashNotes(int userId)
        {
            try
            {
                var noteList = await this.userContext.Notes.Where(x => x.UserId == userId).ToListAsync();
                if (noteList.Count() != 0)
                {
                    return noteList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns> return string after successful adding image</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel> AddImage(int notesId, IFormFile image)
        {
            try
            {
                Account account = new Account(this.configuration.GetValue<string>("CloudinaryAccount:CloudName"), this.configuration.GetValue<string>("CloudinaryAccount:Apikey"), this.configuration.GetValue<string>("CloudinaryAccount:Apisecret"));
                var cloudinary = new Cloudinary(account);
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadparams);
                string imagePath = uploadResult.Url.ToString();
                var findNote = await this.userContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (findNote != null)
                {
                    findNote.Image = imagePath;
                    this.userContext.Notes.Update(findNote);
                    await this.userContext.SaveChangesAsync();
                    return findNote;
                }

                return null;
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
        /// <returns>return string after successful removing image</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<NotesModel>RemoveImage(int notesId)
        {
            try
            {
                var exist = this.userContext.Notes.Find(notesId);
                if (exist != null)
                {
                    exist.Image = null;
                    this.userContext.Notes.Update(exist);
                    await this.userContext.SaveChangesAsync();
                    return exist;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}