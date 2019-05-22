namespace MyPlace.Areas.Mappers
{
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NotesViewModelMapper : IViewModelMapper<List<Note>, NotesViewModel>
    {
        private readonly IViewModelMapper<Note, NoteViewModel> _noteMapper;
       

        public NotesViewModelMapper(
            IViewModelMapper<Note, NoteViewModel> noteMapper)
        {
            _noteMapper = noteMapper ?? throw new ArgumentNullException(nameof(noteMapper));
           
        }

        public NotesViewModel MapFrom(List<Note> entity)
        {
            return new NotesViewModel
            {
                Notes =  entity.Select(_noteMapper.MapFrom).ToList()

            };
        }
    }
}
