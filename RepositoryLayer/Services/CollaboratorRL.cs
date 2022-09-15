using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollaboratorRL : ICollaboratorRL
    {
        readonly FundooContext fundooContext;
        private IConfiguration _config;
        public CollaboratorRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this._config = config;
        }
        public async Task<Collaborator> AddCollaborator(int userId, int NoteId, string emailid)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.userId == userId).FirstOrDefault();
                var note = fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                Collaborator collaborator = new Collaborator();
                if (note.isTrash == true)
                {
                    return null;
                }
                collaborator.userId = userId;
                collaborator.NoteId = NoteId;
                collaborator.CollabEmail = emailid;
                fundooContext.Add(collaborator);
                await fundooContext.SaveChangesAsync();
                return collaborator;

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
                var collab = await fundooContext.Collaborators.Where(x => x.userId == userId && x.NoteId == NoteId).Include(x => x.Note).Include(x => x.user).ToListAsync();

                return collab;
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
                var collab = await fundooContext.Collaborators.Where(x => x.userId == userId).Include(x => x.user).Include(x => x.Note).ToListAsync();

                return collab;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
