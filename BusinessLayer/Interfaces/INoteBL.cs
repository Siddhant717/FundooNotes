using CommonLayer.Model;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        void AddNote(int userId, NoteModel noteModel);
        public void UpdateNote(int userId, int NoteId,UpdateNoteModel updateNoteModel);
    }
}
