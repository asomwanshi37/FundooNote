using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModel;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext UserContext;
        private readonly IConfiguration Configuration;

        public NotesRepository(UserContext userContext, IConfiguration configuration)
        {
            this.UserContext = userContext;
            this.Configuration = configuration;
        }
        public async Task<NotesModel> AddNotes(NotesModel notesModel)
        {
            try
            {
                if (notesModel != null)
                {
                    await this.UserContext.Notes.AddAsync(notesModel);
                    await this.UserContext.SaveChangesAsync();
                    return notesModel;
                }
                return null;
                
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel)
        {
            try
            {
                var exists = this.UserContext.Notes.Where(x => x.NotesId == notesModel.NotesId).SingleOrDefault();
                if (exists != null)
                {
                    exists.Notes = notesModel.Notes;
                    exists.Title = notesModel.Title;
                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UpdateColor(int noteId, string color)
        {
            try
            {
                var exists = this.UserContext.Notes.Where(x => x.NotesId == noteId).SingleOrDefault();
                if (exists != null && color != null)
                {
                        exists.Color = color;
                        this.UserContext.Notes.Update(exists);
                       await this.UserContext.SaveChangesAsync();
                        return exists;
                }
                return null;

            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UpdateArchive(int notesId)
        {
            try
            {
                var exists = this.UserContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefault();
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

                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
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
        public async Task<NotesModel>AddPin(int notesId)
        {
            try
            {
                var exists = this.UserContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefault();
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

                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
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
        public async Task<NotesModel>DeleteAddToTrash(int notesId)
        {
            try
            {
                var exists = this.UserContext.Notes.Where(x => x.NotesId == notesId && x.Is_Trash == false).SingleOrDefault();
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

                    exists.Remainder = null;
                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync ();
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
        public async Task<IEnumerable<NotesModel>>GetNotes(int userId)
        {
            try
            {
                var NoteList = await this.UserContext.Notes.Where(x => x.UserId == userId && x.Is_Archive == false && x.Is_Trash == false).ToListAsync();
                if (NoteList != null)
                {
                    return NoteList;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<NotesModel>RestoreFromTrash(int notesId)
        {
            try
            {
                var exists = await this.UserContext.Notes.Where(x => x.NotesId == notesId && x.Is_Trash == true).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Is_Trash = false;
                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> DeleteaNoteFromTrash(int notesId)
        {
            try
            {
                var exists = await this.UserContext.Notes.Where(x => x.NotesId == notesId && x.Is_Trash == true).SingleOrDefaultAsync();
                if (exists != null)
                {
                    this.UserContext.Notes.Remove(exists);
                    await this.UserContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> SetRemainder(int notesId, string remainder)
        {
            try
            {
                var exists = await this.UserContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Remainder = remainder;
                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
                    return exists;
                }
                else
                {
                    NotesModel note = new NotesModel() { NotesId = notesId, Remainder = remainder };
                    this.UserContext.Notes.Add(note);
                    await this.UserContext.SaveChangesAsync ();
                    return null;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel>DeleteRemainder(int notesId)
        {
            try
            {
                var exists = await this.UserContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (exists != null)
                {
                    exists.Remainder = null;
                    this.UserContext.Notes.Update(exists);
                    await this.UserContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetRemainderNotes(int userId)
        {
            try
            {
                var NoteList = await this.UserContext.Notes.Where(x => x.UserId == userId && x.Remainder != null).ToListAsync();
                if (NoteList.Count() != 0)
                {
                    return NoteList;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetArchiveNotes(int userId)
        {
            try
            {
                var NoteList = await this.UserContext.Notes.Where(x => x.UserId == userId && x.Is_Archive == true).ToListAsync();
                if (NoteList.Count() != 0)
                {
                    return NoteList;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetTrashNotes(int userId)
        {
            try
            {
                var NoteList = await this.UserContext.Notes.Where(x => x.UserId == userId && x.Is_Trash == true).ToListAsync ();
                if (NoteList.Count() != 0)
                {
                    return NoteList;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel>AddImage(int notesId, IFormFile image)
        {
            try
            {
                Account account = new Account(this.Configuration.GetValue<string>("CloudinaryAccount:CloudName"), this.Configuration.GetValue<string>("CloudinaryAccount:Apikey"), this.Configuration.GetValue<string>("CloudinaryAccount:Apisecret"));
                var cloudinary = new Cloudinary(account);
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadparams);
                string imagePath = uploadResult.Url.ToString();
                var findNote = await this.UserContext.Notes.Where(x => x.NotesId == notesId).SingleOrDefaultAsync();
                if (findNote != null)
                {
                    findNote.Notes = imagePath;
                    this.UserContext.Notes.Update(findNote);
                    await this.UserContext.SaveChangesAsync();
                    return findNote;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> RemoveImage(int notesId)
        {
            try
            {
                var exist = this.UserContext.Notes.Find(notesId);
                if (exist != null)
                {
                    exist.Image = null;
                    this.UserContext.Notes.Update(exist);
                    await this.UserContext.SaveChangesAsync();
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
