using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool DeleteNote(int userId, int NoteId)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note == null)
                {
                    return false;
                }
                fundooContext.Notes.Remove(note);
                fundooContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNote( int userId, int NoteId, UpdateNoteModel updateNoteModel)
        {
            try
            {
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();

                note.Title = updateNoteModel.Title != "string" ? updateNoteModel.Title : note.Title;
                note.Description = updateNoteModel.Description != "string" ? updateNoteModel.Description : note.Description;
                note.Color = updateNoteModel.Color != "string" ? updateNoteModel.Color : note.Color;
                note.isPin = updateNoteModel.isPin;
                note.isRemainder = updateNoteModel.isRemainder;
                note.isArchieve = updateNoteModel.isArchieve;
                note.isTrash = updateNoteModel.isArchieve;
                note.Remainder = updateNoteModel.Remainder;
                note.ModifiedDate = DateTime.Now;
                fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
