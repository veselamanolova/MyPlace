namespace MyPlace.Areas.Notes.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Areas.Mappers;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Notes")]
    //  [Authorize(Roles = "Manager")]
    public class NotesController : Controller
    {
        private readonly INoteService _notesService;
        private readonly IMapper _mapper;
        private readonly IUserEntitiesService _userEntitiesService;
        private readonly IEntityCategoriesService _entityCategoriesService;

        public NotesController(INoteService notesService, IMapper mapper, IUserEntitiesService userEntitiesService, IEntityCategoriesService entityCategoriesService)
        {
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userEntitiesService = userEntitiesService ?? throw new ArgumentNullException(nameof(userEntitiesService));
            _entityCategoriesService = entityCategoriesService ?? throw new ArgumentNullException(nameof(entityCategoriesService));  
        }

        //[Authorize(Roles = "Manager")]
        [HttpGet("Notes")]
        public async Task<IActionResult> Notes(int? entityId, string searchedStringInText, int? categoryId)
        {
            string idClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            if (HttpContext.User == null || !HttpContext.User.HasClaim(x => x.Type == idClaimType))
            {
                throw new Exception("Cannot determine current user id");
            }

            string userId = HttpContext.User.FindFirst(idClaimType).Value;
            var userName = HttpContext.User.Identity.Name;  

            var entities = await _userEntitiesService.GetAllUserEntitiesAsync(userId);

            var selectedEntityId = entityId ?? entities[0].EntityId;

            var selectedEntityCategories = await _entityCategoriesService.GetAllEntityCategoriesAsync(selectedEntityId);

            var notes = await _notesService.SearchAsync(selectedEntityId, searchedStringInText, categoryId);

            var entityCategories = _mapper.Map<List<EntityCategoryDTO>, List<CategoryViewModel>>(selectedEntityCategories); 
            AddNoteViewModel addNoteVm = new AddNoteViewModel()
            {
                Note = new NoteViewModel
                {
                    EntityId = selectedEntityId, 
                    NoteUser = new NoteUserViewModel
                    {
                        Id = userId,
                        Name = userName
                    }
                },
                EntityCategories = entityCategories,
                
            };



            NotesViewModel vm = new NotesViewModel()
            {
                UserEntites = _mapper.Map<List<UserEntityDTO>, List<UserEntityViewModel>>(entities),
                SelectedEntityId = selectedEntityId,
                // Notes = //_mapper.Map<List<NoteDTO>, List<NoteViewModel>>(notes),
                Notes = notes.Select(x => new NoteViewModel
                {
                    Id = x.Id,
                    EntityId = x.EntityId,
                    NoteUser = new NoteUserViewModel
                    {
                        Id = x.NoteUser.Id,
                        Name = x.NoteUser.Name
                    },
                    Text = x.Text,
                    Date = x.Date,
                    Category = new CategoryViewModel
                    {
                        CategoryId = x.Category.CategoryId,
                        Name = x.Category.Name
                    },
                    CurrentUserId = userId
                }).ToList(),
                AddNote = addNoteVm,
                SearchNotes = new SearchNotesViewModel
                {
                    EntityId = selectedEntityId,
                    EntityCategories = entityCategories
                }
            };
            
           


            return View(vm);
        }

       // [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote(AddNoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(nameof(Notes), model);
            }

            try
            {               
                await _notesService.AddAsync(model.Note.EntityId, model.Note.NoteUser.Id,  model.Note.Text, model.SelectedCategoryId);
                return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });                
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Notes), model);
            }
        }

        //[ValidateAntiForgeryToken]
        //[HttpGet("SearchNote")]
        public IActionResult SearchNote(SearchNotesViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(nameof(Notes), model);
            }

            try
            {
               
                return RedirectToAction(nameof(Notes), new
                {
                    entityId = model.EntityId,
                    searchedStringInText = model.SearchedStringInText,
                    categoryId = model.SearchCategoryId
                });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Notes), model);
            }
        }
    }
}
