namespace MyPlace.Areas.Notes.Models
{
    using MyPlace.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NoteViewModel
    {
        public int Id { get; set; }

        public int EntityId { get; set; }

        [Required(ErrorMessage = "note text is required")]
        public string Text { get; set; }

        public Category Category { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime Date { get; set; }
    }
}

