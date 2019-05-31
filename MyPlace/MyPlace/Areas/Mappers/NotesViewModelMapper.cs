
namespace MyPlace.Areas.Mappers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using MyPlace.DataModels;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Services.DTOs;

    //public class NotesViewModelMapper : IViewModelMapper<List<NoteDTO>, NotesViewModel>
    //{
    //    private readonly IViewModelMapper<NoteDTO, NoteViewModel> _noteMapper;
       
    //    public NotesViewModelMapper(
    //        IViewModelMapper<NoteDTO, NoteViewModel> noteMapper)
    //    {
    //        _noteMapper = noteMapper ?? throw new ArgumentNullException(nameof(noteMapper));
    //    }

    //    public NotesViewModel MapFrom(List<NoteDTO> entity) =>
    //        new NotesViewModel
    //        {
    //            Notes = entity.Select(x=>_noteMapper.MapFrom(x)).ToList()
    //        };
    //}
}
