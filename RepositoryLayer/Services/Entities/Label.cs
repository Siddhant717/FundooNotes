using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Label
    {
        public string LabelName { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }


        public int userId { get; set; }
        public User user { get; set; }

    }      
        
}
