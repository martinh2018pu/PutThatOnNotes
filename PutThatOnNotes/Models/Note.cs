using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PutThatOnNotes.Models
{
    public class Note : BaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public bool IsActive { get; set; }


        public virtual List<Comment> Comments { get; set; }
    }
}
