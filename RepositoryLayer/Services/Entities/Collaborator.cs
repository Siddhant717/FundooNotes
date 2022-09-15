using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Collaborator
    {
        [Key]
        public int CollabId { get; set; }
        public string CollabEmail { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
