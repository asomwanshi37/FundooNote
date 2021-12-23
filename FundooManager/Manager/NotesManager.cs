using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NotesManager: INotesManager
    {
        private readonly INotesRepository notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }
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
        public async Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel)
        {
            try
            {
                return await this.notesRepository.UpdateTitleOrNote(notesModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
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
        public async Task<NotesModel> DeleteaNoteFromTrash(int notesId)
        {
            try
            {
                return await this.notesRepository.DeleteaNoteFromTrash(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<NotesModel> SetRemainder(int notesId, string remainder)
        {
            try
            {
                return await this.notesRepository.SetRemainder(notesId, remainder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> DeleteRemainder(int notesId)
        {
            try
            {
                return await this.notesRepository.DeleteRemainder(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetRemainderNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetRemainderNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
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
