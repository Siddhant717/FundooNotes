using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        readonly FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
            

        public void AddNote(int userId, NoteModel noteModel)
        {
            try
            {
                Note note = new Note();
                note.userId = userId;
                note.Title = noteModel.Title;
                note.Description = noteModel.Description ;
                note.Color = noteModel.Color;
                note.Remainder = DateTime.Now;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                
                fundooContext.Notes.Add(note);
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
