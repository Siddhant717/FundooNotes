using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetLabelModel
    {
        public string LabelName { get; set; }
        public int NoteId { get; set; }
        public int userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
