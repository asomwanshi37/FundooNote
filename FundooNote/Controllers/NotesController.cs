// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNote.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// NotesController Class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes manager
        /// </summary>
        private readonly INotesManager notesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesManager">The notes manager.</param>
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>returns a IActionResult as status code when data added successful</returns>
        [HttpPost]
        [Route("api/addNote")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                NotesModel valid = await this.notesManager.AddNotes(notesModel);
                if (valid != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Please Check Your Note Added", Data = valid });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Updates the title or note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return IActionResult as status code on successful update of data for title or Note</returns>
        [HttpPut]
        [Route("api/UpdateTitleOrNote")]
        public async Task<IActionResult> UpdateTitleOrNote([FromBody] NotesModel notesModel, int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.UpdateTitleOrNote(notesModel, notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Your Note Update Properly", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not Update", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Updates the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>return IActionResult as status code on successful update of color</returns>
        [HttpPut]
        [Route("api/UpdateColor")]
        public async Task<IActionResult> UpdateColor(int noteId, string color)
        {
            try
            {
                NotesModel result = await this.notesManager.UpdateColor(noteId, color);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = " Colour Update ", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Colour Not Update" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Updates the archive.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return the IActionResult as status code after updating archive</returns>
        [HttpPut]
        [Route("api/archive")]
        public async Task<IActionResult> UpdateArchive(int notesId)
        {
            try
            {
                NotesModel note = await this.notesManager.UpdateArchive(notesId);
                if (note != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Note Archive Sucessful", Data = note });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not Archive " });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Adds the pin.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a IActionResult as status code after updating pin</returns>
        [HttpPut]
        [Route("api/Pin")]
        public async Task<IActionResult> AddPin(int notesId)
        {
            try
            {
                NotesModel pin = await this.notesManager.AddPin(notesId);
                if (pin != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Note Pinned Sucessful", Data = pin });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not Pinned" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Deletes the add to trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return IActionResult as status code on adding notes to trash after delete</returns>
        [HttpPut]
        [Route("api/Trash")]
        public async Task<IActionResult> DeleteAddToTrash(int notesId)
        {
            try
            {
                NotesModel trash = await this.notesManager.DeleteAddToTrash(notesId);
                if (trash != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Note trashed", Data = trash });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note Not present! Add Note" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return IActionResult as status code</returns>
        [HttpPost]
        [Route("api/GetNotes")]
        public async Task<IActionResult> GetNotes(int userId)
        {
            try
            {
                var notes = await this.notesManager.GetNotes(userId);
                if (notes != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = "Retrieved notes successful!", Data = notes });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No Notes present" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Restores from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a IActionResult on successful restore</returns>
        [HttpPut]
        [Route("api/Trash/Restore")]
        public async Task<IActionResult> RestoreFromTrash(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.RestoreFromTrash(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Removed from trash !", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "from Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Delete the note from trash.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a IActionResult on successful delete</returns>
        [HttpDelete]
        [Route("api/Trash/Delete")]
        public async Task<IActionResult> DeleteNoteFromTrash(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteNoteFromTrash(notesId);
                if (result == null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Deleted Data Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Data Not Delete" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="reminder">The remainder.</param>
        /// <returns>return IActionResult as status on successful remainder set</returns>
        [HttpPost]
        [Route("api/Reminder")]
        public async Task<IActionResult> SetReminder(int notesId, string reminder)
        {
            try
            {
                NotesModel data = await this.notesManager.SetReminder(notesId, reminder);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Remainder Set", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Remainder Not Set" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Delete the reminder.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return IActionResult status after removing the reminder</returns>
        [HttpPut]
        [Route("api/ReminderDelete")]
        public async Task<IActionResult> DeleteReminder(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteReminder(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Remainder Removed", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Remainder Not Removed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the reminder notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return IActionResult status after get reminder</returns>
        [HttpPost]
        [Route("api/GetReminderNotes")]
        public async Task<IActionResult> GetReminderNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetReminderNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Retrived Reminder Note Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Reminder Notes not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return IActionResult Status result as get archive notes</returns>
        [HttpPost]
        [Route("api/GetArchiveNotes")]
        public async Task<IActionResult> GetArchiveNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetArchiveNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Retrived Archived Notes Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Archived Notes not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the trash notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns IActionResult Status result get trash notes</returns>
        [HttpPost]
        [Route("api/GetTrashNotes")]
        public async Task<IActionResult> GetTrashNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetTrashNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>() { Status = true, Message = "Trash retrived successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Trash is Empty" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>return string after successful adding image</returns>
        [HttpPut]
        [Route("api/addImage")]
        public async Task<IActionResult> AddImage(int notesId, IFormFile image)
        {
            try
            {
                NotesModel data = await this.notesManager.AddImage(notesId, image);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Image added", Data = data });
                }

                return this.BadRequest(new { Status = false, Message = "Image Not added" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = true, ex.Message });
            }
        }

        /// <summary>
        /// Removes the image.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return string after successful removing image</returns>
        [HttpPut]
        [Route("api/removeImage")]
        public async Task<IActionResult> RemoveImage(int notesId)
        {
            try
            {
                NotesModel image = await this.notesManager.RemoveImage(notesId);
                if (image == null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Image removed", Data = image });
                }

                return this.BadRequest(new { Status = false, Message = "Image Not Present" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = true, ex.Message });
            }
        }
    }
}
