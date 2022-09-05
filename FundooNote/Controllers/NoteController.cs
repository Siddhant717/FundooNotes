﻿using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        INoteBL noteBL;

        private IConfiguration _config;
        private FundooContext fundooContext;
        public NoteController(INoteBL noteBL, IConfiguration config, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this._config = config;
            this.fundooContext = fundooContext;

        }
        [Authorize]
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                //Authorization, match userid from token
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                this.noteBL.AddNote(UserID, noteModel);
                return this.Ok(new { success = true, status = 200, message = "Note Added Sucessfully" });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public IActionResult UpdateNote(int NoteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userId.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this.noteBL.UpdateNote(UserID, NoteId, updateNoteModel);
                return this.Ok(new { success = true, status = 200, message = "Note Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public IActionResult DeleteNote(int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this.noteBL.DeleteNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public IActionResult GetNote(int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note not exist" });
                }
                Note notes = new Note();
                notes = this.noteBL.GetNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, note = notes });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNote")]
        public IActionResult GetAllNote()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<Note> notes = new List<Note>();
                notes = this.noteBL.GetAllNotes(UserID);
                return this.Ok(new { success = true, status = 200, Allnotes = notes });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet("GetAllNoteByUsingJoin")]
        public IActionResult GetAllNotesByUsingJoin()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<NoteResponseModel> notes = new List<NoteResponseModel>();
                notes = this.noteBL.GetAllNotesByUsingJoin(UserID);
                return this.Ok(new { success = true, status = 200, Allnotes = notes });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("ArchieveNote/{NoteId}")]
        public async Task<IActionResult> ArchieveNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var archieve = await this.noteBL.ArchieveNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note archieved successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("PinNote/{NoteId}")]
        public async Task<IActionResult> PinNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var pin = await this.noteBL.PinNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Pinned successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Authorize]
        [HttpPut("TrashNote/{NoteId}")]
        public async Task<IActionResult> TrashNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                bool trash = await this.noteBL.TrashNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Trashed Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ReminderNote/{NoteId}")]
        public async Task<IActionResult> ReminderNote(int NoteId, NoteReminderModel reminder)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var rem = Convert.ToDateTime(reminder.Reminder);
                await this.noteBL.ReminderNote(UserID, NoteId, rem);
                return this.Ok(new { success = true, status = 200, message = "Note  reminder added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteReminder/{NoteId}")]
        public async Task<IActionResult> DeleteReminderNote(int NoteId)
        {
            try
            {
                var note = await fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                await this.noteBL.DeleteReminder(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Reminder Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
