namespace MyPlace.Areas.Notes.Models
{
    using System.Collections.Generic;

    public class NotesViewModel
    {
        public List<UserEntityViewModel> UserEntites { get; set; }

        public int SelectedEntityId { get; set; }

        public string UserId { get; set; }

        public PaginatedList<NoteViewModel> Notes { get; set; }

        //public List<NoteViewModel> Notes{ get; set; }

        public AddNoteViewModel AddNote { get; set;  }

        public SearchNotesViewModel SearchNotes { get; set; }

        public string PreviousPageLink { get; set; }

        public string NextPageLink { get; set; }
    }
}
