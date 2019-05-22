namespace MyPlace.Areas.Notes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Areas.Mappers;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;

    [Area("Notes")]
    //  [Authorize(Roles = "Manager")]
    public class NotesController : Controller
    {
        private readonly INoteService _notesService;
        private readonly IMapper _mapper;
        private readonly IUserEntitiesService _userEntitiesService;

        public NotesController(INoteService notesService, IMapper mapper, IUserEntitiesService userEntitiesService)
        {
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userEntitiesService = userEntitiesService ?? throw new ArgumentNullException(nameof(userEntitiesService));
        }

        // [Authorize(Roles = "Manager")]
        [HttpGet("Notes")]
        public async Task<IActionResult> Notes(int? entityId, int? noteId)
        {
            string idClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            if (HttpContext.User == null || !HttpContext.User.HasClaim(x => x.Type == idClaimType))
            {
                throw new Exception("Cannot determine current user id");
            }

            string userId = HttpContext.User.FindFirst(idClaimType).Value;
            var entities = await _userEntitiesService.GetAllUserEntitiesAsync(userId);

            var selectedEntityId = entityId ?? entities[0].EntityId;

            //  var selectedEntityCategories = 

            var notes = await _notesService.GetAllAsync(selectedEntityId);

                      

            AddNoteViewModel addNoteVm = new AddNoteViewModel()
            {
                Note = _mapper.Map<Note, NoteViewModel>(new Note()),
                EntityCategories = new List<CategoryViewModel>()
            }; 

            NotesViewModel vm = new NotesViewModel()
            {
                UserEntites = _mapper.Map<List<UserEntityDTO>, List<UserEntityViewModel>>(entities),
                SelectedEntityId = selectedEntityId,
                Notes = _mapper.Map<List<Note>, List<NoteViewModel>>(notes),
                AddNote = addNoteVm
            };        
            

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
        public async Task<IActionResult> _AddNotePartial(AddNoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _notesService.AddAsync(model.Note.EntityId, model.Note.Text, model.Note.Category.Id);
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
