namespace MyPlace.Areas.Notes.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MyPlace.DataModels;

    public class NoteViewModel
    {
        public int Id { get; set; }

        public int EntityId { get; set; }

        public NoteUserViewModel NoteUser { get; set; }

        
        [Required(ErrorMessage = "Note text is required. It can contain words, numbers and punctuation."),
            RegularExpression("^[a-zA-Z0-9,.!?;: ]*$")]        
        public string Text { get; set; }

        public CategoryViewModel Category { get; set; }

        public bool IsCompleted { get; set; }

        public bool HasStatus { get; set; }

        public DateTime Date { get; set; }

        public string CurrentUserId { get; set; }

    }
}

