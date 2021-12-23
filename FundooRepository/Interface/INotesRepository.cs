using FundooModel;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface INotesRepository 
    {
        Task<NotesModel> AddNotes(NotesModel notesModel);
        Task<NotesModel> UpdateTitleOrNote(NotesModel notesModel);
        Task<NotesModel> UpdateColor(int noteId, string color);
        Task<NotesModel> UpdateArchive(int notesId);
        Task<NotesModel> AddPin(int notesId);
        Task<NotesModel> DeleteAddToTrash(int notesId);
        Task<IEnumerable<NotesModel>> GetNotes(int userId);
        Task<NotesModel> RestoreFromTrash(int notesId);
        Task<NotesModel> DeleteaNoteFromTrash(int notesId);
        Task<NotesModel> SetRemainder(int notesId, string remainder);
        Task<NotesModel> DeleteRemainder(int notesId);
        Task<IEnumerable<NotesModel>> GetRemainderNotes(int userId);
        Task<IEnumerable<NotesModel>> GetArchiveNotes(int userId);
        Task<IEnumerable<NotesModel>> GetTrashNotes(int userId);
        Task<NotesModel> AddImage(int notesId, IFormFile image);
        Task<NotesModel> RemoveImage(int notesId);
    }
}
