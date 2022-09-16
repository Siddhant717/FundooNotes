using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorController : Controller
    {
        ICollaboratorBL collaboratorBL;
        private readonly IConfiguration _config;
        private readonly FundooContext fundooContext;

        public CollaboratorController(ICollaboratorBL collaboratorBL, IConfiguration config, FundooContext fundooContext)
        {
            this.collaboratorBL = collaboratorBL;
            this._config = config;
            this.fundooContext = fundooContext;

        }
        [Authorize]
        [HttpPost("AddCollaborator/{NoteId}/{emailid}")]
        public async Task<IActionResult> AddCollaborator(int NoteId, string emailid)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note == null)
                {
                    return BadRequest(new { success = false, message = "Note not found" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                await collaboratorBL.AddCollaborator(UserID, NoteId, emailid);
                return this.Ok(new { success = true, status = 200, message = "Collaborator added successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("GetCollaboratorByNoteId/{NoteId}")]
        public async Task<IActionResult> GetCollaboratorByNoteId(int NoteId)
        {
            try
            {
                var collab = fundooContext.Collaborators.FirstOrDefault(x => x.NoteId == NoteId);
                if (collab == null)
                {
                    return BadRequest(new { success = false, message = "collaborator not exist" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                var collablist = await collaboratorBL.GetCollaboratorByNoteId(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, data = collablist });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCollaboratorByUserId")]
        public async Task<IActionResult> GetCollaboratorByUserId()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                var collablist = await collaboratorBL.GetCollaboratorByUserId(UserID);
                return this.Ok(new { success = true, status = 200, data = collablist });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("RemoveCollaborator/{NoteId}/{collabId}")]
        public async Task<IActionResult> RemoveCollaborator(int NoteId, int collabId)
        {
            try
            {
                var collab = fundooContext.Collaborators.FirstOrDefault(x => x.NoteId == NoteId);
                if (collab == null)
                {
                    return BadRequest(new { success = false, message = "No such Note exist" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                bool isExist = await collaboratorBL.RemoveCollaborator(UserID, NoteId, collabId);
                if(isExist) return this.Ok(new { success = true, status = 200, message = $"Collaborator Deleted successfully" });
                else return BadRequest(new { success = false, message = "Collaborator doesn't exist" });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}