using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        [HttpPost]
        [Route("api/addNote")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel notesModel)
        {
            try
            {
                NotesModel valid = await this.notesManager.AddNotes(notesModel);
                if (valid != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Please Check Your Note Added",Data = valid });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<NotesModel>() { Status = false, Message = "Note Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<NotesModel>() { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/UpdateNote")]
        public async Task<IActionResult> UpdateTitleOrNote([FromBody] NotesModel notesModel)
        {
            try
            {
                NotesModel result = await this.notesManager.UpdateTitleOrNote(notesModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Your Note Update Properly",Data=result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message ="Note Not Update" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/UpdateColor")]
        public async Task<IActionResult> UpdateColor(int noteId, string color)
        {
            try
            {
                NotesModel result = await this.notesManager.UpdateColor(noteId, color);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message =" Colour Update " ,Data=result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Colour Not Update" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

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
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note Not Archive " });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/Pin")]
        public async Task<IActionResult> AddPin(int notesId)
        {
            try
            {
                NotesModel pin = await this.notesManager.AddPin(notesId);
                if (pin != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message ="Note Pinned Sucessful",Data=pin });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note Not Pinned"});
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/Trash")]
        public async Task<IActionResult> DeleteAddToTrash(int notesId)
        {
            try
            {
                NotesModel trash = await this.notesManager.DeleteAddToTrash(notesId);
                if (trash != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Note trashed", Data=trash });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Note Not present! Add Note" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/GetNotes")]
        public async Task<IActionResult> GetNotes(int userId)
        {
            try
            {
                var notes = await this.notesManager.GetNotes(userId);
                if (notes != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = "Retrieved notes successful!", Data=notes});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No Notes present" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/Trash/Restore")]
        public async Task<IActionResult> RestoreFromTrash(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.RestoreFromTrash(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Removed from trash !" , Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "from Not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/Trash/Delete")]
        public async Task<IActionResult> DeleteaNoteFromTrash(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteaNoteFromTrash(notesId);
                if (result == null)
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Deleted Data Successfully" , Data = result});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message ="Data Not Delete" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Remainder")]
        public async Task<IActionResult> SetRemainder(int notesId, string remainder)
        {
            try
            {
                NotesModel data = await this.notesManager.SetRemainder(notesId, remainder);
                if (data != null )
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Remainder Set",Data=data });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Remainder Not Set" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/RemainderDelete")]
        public async Task<IActionResult> DeleteRemainder(int notesId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteRemainder(notesId);
                if (result != null )
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Remainder Removed",Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Remainder Not Removed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/GetRemainderNotes")]
        public async Task<IActionResult> GetRemainderNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetRemainderNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = "Retrived Reminder Note Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>{ Status = false, Message = "Reminder Notes not Available"});
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/GetArchiveNotes")]
        public async Task<IActionResult> GetArchiveNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetArchiveNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = "Retrived Archived Notes Sucessfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Archived Notes not Available" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/GetTrashNotes")]
        public async Task<IActionResult> GetTrashNotes(int userId)
        {
            try
            {
                var result = await this.notesManager.GetTrashNotes(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = "Trash retrived successfully",Data =result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false,Message = "Trash is Empty" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false,Message = ex.Message });
            }

        }

        [HttpPut]
        [Route("api/addImage")]
        public async Task<IActionResult> AddImage(int notesId, IFormFile image)
        {
            try
            {
                NotesModel data = await this.notesManager.AddImage(notesId, image);
                if (data != null )
                {
                    return this.Ok(new ResponseModel<NotesModel>() { Status = true, Message = "Image added",Data = data });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not added" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }

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

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Image Not Present" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = true, Message = ex.Message });
            }
        }
    }
}
