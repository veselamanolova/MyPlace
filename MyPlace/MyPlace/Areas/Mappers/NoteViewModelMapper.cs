
namespace MyPlace.Areas.Mappers
{
    using MyPlace.Areas.Notes.Models;
    using MyPlace.DataModels;

    public class NoteViewModelMapper : IViewModelMapper<Note, NoteViewModel>
    {
        public NoteViewModel MapFrom(Note note) =>
            new NoteViewModel()
            {
                Id = note.Id,
                EntityId = note.EntityId,
                Text = note.Text,
                Date = note.Date,
                Category = note.Category
            };
    }
}
