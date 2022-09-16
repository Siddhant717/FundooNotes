using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL collaboratorRL;
        public CollaboratorBL(ICollaboratorRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }
        public async Task<Collaborator> AddCollaborator(int userId, int NoteId, string emailid)
        {
            try
            {
                return await collaboratorRL.AddCollaborator(userId, NoteId, emailid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByNoteId(int userId, int NoteId)
        {
            try
            {
                return await collaboratorRL.GetCollaboratorByNoteId(userId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorByUserId(int userId)
        {
            try
            {
                return await collaboratorRL.GetCollaboratorByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveCollaborator(int userId, int NoteId, int collabId)
        {
            try
            {
                return await collaboratorRL.RemoveCollaborator(userId, NoteId, collabId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
