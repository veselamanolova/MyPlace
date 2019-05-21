


namespace MyPlace.Areas.Mappers
{
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Data.Models;

    public class NoteViewModelMapper : IViewModelMapper<Note, NoteViewModel>
    {
        public NoteViewModel MapFrom(Note note)
        {
            return new NoteViewModel()
            {
                Id = note.Id,
                EntityId = note.EntityId,
                Text = note.Text,
                Date = note.Date,
                Category = note.Category
            }; 
        }
    }
}
