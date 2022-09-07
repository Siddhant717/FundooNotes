using CommonLayer.Model;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int NoteId, string labelName);
        Task<Label> GetLabelByNoteId(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId);
        Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int UserId);
    }
}


