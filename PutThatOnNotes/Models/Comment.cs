using System;
using System.ComponentModel.DataAnnotations;

namespace PutThatOnNotes.Models
{
    public class Comment : BaseModel
    {
        public int NoteId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }


        public virtual Note Note { get; set; }
    }
}
