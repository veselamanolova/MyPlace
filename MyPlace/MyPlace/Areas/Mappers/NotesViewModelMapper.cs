
namespace MyPlace.Areas.Mappers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using MyPlace.DataModels;
    using MyPlace.Areas.Notes.Models;

    public class NotesViewModelMapper : IViewModelMapper<List<Note>, NotesViewModel>
    {
        private readonly IViewModelMapper<Note, NoteViewModel> _noteMapper;
       
        public NotesViewModelMapper(
            IViewModelMapper<Note, NoteViewModel> noteMapper)
        {
            _noteMapper = noteMapper ?? throw new ArgumentNullException(nameof(noteMapper));
        }

        public NotesViewModel MapFrom(List<Note> entity) =>
            new NotesViewModel
            {
                Notes = entity.Select(_noteMapper.MapFrom).ToList()
            };
    }
}
