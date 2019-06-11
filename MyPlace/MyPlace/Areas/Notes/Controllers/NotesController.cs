
namespace MyPlace.Areas.Notes.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;    
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using System.Globalization;
    using AutoMapper;

    [Area("Notes")]
    public class NotesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INoteService _notesService;
        private readonly IUserEntitiesService _userEntitiesService;
        private readonly IEntityCategoriesService _entityCategoriesService;

        public NotesController(INoteService notesService, IMapper mapper,
            IUserEntitiesService userEntitiesService, IEntityCategoriesService entityCategoriesService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _notesService = notesService ?? throw new ArgumentNullException(nameof(notesService));
            _userEntitiesService = userEntitiesService ?? throw new ArgumentNullException(nameof(userEntitiesService));
            _entityCategoriesService = entityCategoriesService ?? throw new ArgumentNullException(nameof(entityCategoriesService));
        }

        [HttpGet("Notes")] 
        public async Task<IActionResult> Notes(int? entityId, string searchedStringInText,
            int? categoryId, string exactDate,
            string fromDate, string toDate, string creator, string sortOption,bool sortIsAscending,  int? pageNumber)
        {

            string userId = GetLoggedUserId();
            var userName = HttpContext.User.Identity.Name;

            var entities = await _userEntitiesService.GetAllUserEntitiesAsync(userId);

            var selectedEntityId = entityId ?? entities[0].EntityId;


            List<CategoryViewModel> entityCategories = await GetEntityCategoriesAsync(selectedEntityId);         
            
          
            int pageSize = 3;
            int pageIndex = pageNumber ?? 1; 
            int? skip = (pageIndex-1) * (pageSize); 
            int? take = pageSize; 
            

            var notesSearchResult = await _notesService.SearchAsync(selectedEntityId, searchedStringInText,
                categoryId, ParseNullableDate(exactDate), ParseNullableDate(fromDate), 
                ParseNullableDate(toDate), creator, sortOption, sortIsAscending,
                skip, pageSize);

            var notes = notesSearchResult.Notes;
            var notesCount = notesSearchResult.NotesCount; 


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

            var previousPageLink = Url.Action(nameof(Notes), "Notes", new
            {
                entityId,
                searchedStringInText,
                categoryId,
                exactDate,
                fromDate,
                toDate,
                creator,
                sortOption,
                sortIsAscending,
                pageNumber = (pageNumber ?? 1) - 1
            });


            var nextPageLink = Url.Action(nameof(Notes), "Notes", new
            {
                entityId,
                searchedStringInText,
                categoryId,
                exactDate,
                fromDate,
                toDate,
                creator,
                sortOption,
                sortIsAscending,
                pageNumber = (pageNumber ?? 1) + 1
            });

            NotesViewModel vm = new NotesViewModel()
            {

                UserEntites = _mapper.Map<List<UserEntityDTO>, List<UserEntityViewModel>>(entities),
                SelectedEntityId = selectedEntityId,
                // Notes = //_mapper.Map<List<NoteDTO>, List<NoteViewModel>>(notes),
                Notes = new PaginatedList<NoteViewModel>(
                    notes.Select(x => ConvertNoteDtoToNoteViewModel(x, userId)).ToList(),
                    notesCount, pageIndex, pageSize),
                AddNote = addNoteVm,
                SearchNotes = new SearchNotesViewModel
                {
                    EntityId = selectedEntityId,
                    EntityCategories = entityCategories,
                    Creator = creator,
                    SearchedStringInText = searchedStringInText,
                    SearchCategoryId = categoryId,
                    ExactDate = ParseNullableDate(exactDate), 
                    FromDate = ParseNullableDate(fromDate),
                    ToDate = ParseNullableDate(toDate),                    
                    SortOption = sortOption, 
                    SortIsAscending = sortIsAscending
                },
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };
            return View(vm);            
        }



        private async Task<List<CategoryViewModel>> GetEntityCategoriesAsync(int entityId)
        {
            var selectedEntityCategories = await _entityCategoriesService.GetAllEntityCategoriesAsync(entityId);
            return _mapper.Map<List<EntityCategoryDTO>, List<CategoryViewModel>>(selectedEntityCategories);
        }

        private string GetLoggedUserId()
        {
            string idClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            if (HttpContext.User == null || !HttpContext.User.HasClaim(x => x.Type == idClaimType))
            {
                throw new Exception("Cannot determine current user id");
            }
            return HttpContext.User.FindFirst(idClaimType).Value;
        }

        private static NoteViewModel ConvertNoteDtoToNoteViewModel(NoteDTO x, string userId)
        {
            return new NoteViewModel
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
                CurrentUserId = userId,
                IsCompleted = x.IsCompleted,
                HasStatus = x.HasStatus
            };
        }

        private static DateTime? ParseNullableDate(string date)
        {
            DateTime dateTimeResult;
            if (!DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateTimeResult))
            {
                return null;
            }
            return dateTimeResult;
        }

        // [Authorize(Roles = "Manager")]
        [HttpPost("AddNote")]       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(AddNoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(nameof(Notes), model);
            }

            try
            {
                await _notesService.AddAsync(model.Note.EntityId, model.Note.NoteUser.Id, model.Note.Text, model.SelectedCategoryId);
                return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Notes), model);
            }
        }


        [HttpGet("Notes/EditNote")]
        public async Task<IActionResult> Edit(int noteId)
        {

            var note = (await _notesService.GetByIdAsync(noteId));
            string userId = GetLoggedUserId();


            EditNoteViewModel model = new EditNoteViewModel
            {
                Note = ConvertNoteDtoToNoteViewModel(note, userId),
                EntityCategories = await GetEntityCategoriesAsync(note.EntityId),
                SelectedCategoryId = note.Category.CategoryId
            };

            return View(nameof(Edit), model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("Notes/DeleteNote")]
        // public async Task<IActionResult> DeleteNote(int noteId, int entityId)
        public async Task<IActionResult> DeleteNote(NoteViewModel model)
        {
            
            await _notesService.DeleteAsync(model.Id);
            return RedirectToAction(nameof(Notes), new { entityId = model.EntityId });
        }


        [ValidateAntiForgeryToken]
        [HttpPost("Notes/EditNote")]
        public async Task<IActionResult> EditNote(EditNoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {                
                return View(nameof(Edit), new { entityId = model.Note.EntityId });
            }

            try
            {
                await _notesService.EditAsync(model.Note.Id, model.Note.Text,
                    model.SelectedCategoryId, model.Note.IsCompleted,
                    model.Note.HasStatus);
                return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Edit), new { entityId = model.Note.EntityId });
            }
        }


        [ValidateAntiForgeryToken]
        public IActionResult SearchNote(SearchNotesViewModel model)
        {
            if (!this.ModelState.IsValid)
                return View(nameof(Notes), model);

            try
            {
                return RedirectToAction(nameof(Notes), new
                {
                    entityId = model.EntityId,
                    searchedStringInText = model.SearchedStringInText,
                    categoryId = model.SearchCategoryId,
                    exactDate = model.ExactDate?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                    fromDate = model.FromDate?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                    toDate = model.ToDate?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                    creator = model.Creator,
                    sortOption = model.SortOption,
                    sortIsAscending = model.SortIsAscending

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
