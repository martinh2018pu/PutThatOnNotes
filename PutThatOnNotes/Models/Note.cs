using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PutThatOnNotes.Models
{
    public class Note : BaseModel
    {
        [Required, MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        // this down data anotation forbids edits even from backend. TBH, not quite sure.
        //[Editable(false)]
        public DateTime CreationDate { get; set; }

        //[Editable(false)]
        public DateTime LastModificationDate { get; set; }

        public bool IsActive { get; set; }


        public virtual List<Comment> Comments { get; set; }
    }
}
