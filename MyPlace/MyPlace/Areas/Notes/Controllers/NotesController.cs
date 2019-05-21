namespace MyPlace.Areas.Notes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;    
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Areas.Mappers;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;

    [Area("Notes")]
  //  [Authorize(Roles = "Manager")]
    public class NotesController : Controller
    {
        private readonly INoteService _notesService;
        private readonly IViewModelMapper<List<Note>, NotesViewModel> _notesMapper;

        public NotesController(INoteService notesService, IViewModelMapper<List<Note>, NotesViewModel> notesMapper)
        {
            _notesService = notesService;
            _notesMapper = notesMapper ?? throw new ArgumentNullException(nameof(notesMapper));
        }

      //  [Authorize(Roles = "Administrator")]
        [HttpGet("Notes")]
        public async Task<IActionResult> Notes(int entityId)
        {
            
            var notes = await _notesService.GetAllAsync(1);

            NotesViewModel vm =  _notesMapper.MapFrom(notes);

            return View(vm);
        }


        //  [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpGet("AddNote")]
        public IActionResult _AddNotePartial(int entityId)
        {
            NoteViewModel vm = new NoteViewModel()
            {
                EntityId = entityId 
            };
           
            return RedirectToAction(nameof(Notes));
        }

        //  [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost("AddNote")]
        public async Task<IActionResult> _AddNotePartial(NoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _notesService.AddAsync(model.EntityId, model.Text, model.Category.Id);
                return View(model);
                // return RedirectToAction(nameof(Notes));
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }

    }
}
