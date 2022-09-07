using CommonLayer.Model;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int userId, int NoteId, string labelName);
        Task<Label> GetLabelByNoteId(int userId, int NoteId);
        Task <List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int userId);
        Task UpdateLabel(int userId, int NoteId, string newLabel);
        Task<bool> DeleteLabel(int userId, int NoteId);
    }
}
