using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRL
    {
        Task<Collaborator> AddCollaborator(int userId, int NoteId, string emailid);
        Task<List<Collaborator>> GetCollaboratorByUserId(int userId);
        Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId);
        Task<bool> RemoveCollaborator(int userId, int NoteId, int collabId);
    }
}
