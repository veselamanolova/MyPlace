namespace MyPlace.Areas.Notes.Models
{
    using MyPlace.Data.Models;
    using System.Collections.Generic;

    public class NotesViewModel
    {
        public List<UserEntityViewModel> UserEntites { get; set; }

        public int SelectedEntityId { get; set; }

        public string UserId { get; set; }

        public List<NoteViewModel> Notes{ get; set; }

        public AddNoteViewModel AddNote { get; set;  }
    }
}
