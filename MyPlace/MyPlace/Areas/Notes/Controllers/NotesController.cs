
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
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Authorization;
    using MyPlace.Common;

    [Area("Notes")]    
    [Authorize(Roles = GlobalConstants.ManagerRole)]
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
            int? categoryId, string exactDate, string fromDate, string toDate, string creator, 
            NotesSearchByStatus searchByStatus, string sortOption, bool sortIsAscending, int? pageNumber)
        {

            string userId = GetLoggedUserId();
            var userName = HttpContext.User.Identity.Name;

            var entities = await _userEntitiesService.GetAllUserEntitiesAsync(userId);
            var selectedEntityId = entityId ?? entities[0].EntityId;
            var entityCategories = await GetEntityCategoriesAsync(selectedEntityId);

            int pageSize = 5;
            int pageIndex = pageNumber ?? 1;
            int? skip = (pageIndex - 1) * (pageSize);
            int? take = pageSize;

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

            NotesSearchResultDTO notesSearchResult = null;
            string errorMessage = null;
            try
            {
                notesSearchResult = await _notesService.SearchAsync(selectedEntityId, searchedStringInText,
                    categoryId, ParseNullableDate(exactDate), ParseNullableDate(fromDate),
                    ParseNullableDate(toDate), creator, searchByStatus, sortOption, sortIsAscending,
                    skip, pageSize);
            }
            catch (ApplicationException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = "Error loading notes. Please try again.";
            }
            NotesViewModel vm = CreateNotesViewModel(searchedStringInText, categoryId, 
                exactDate, fromDate, toDate, 
                creator, searchByStatus, sortOption, sortIsAscending, 
                entities, selectedEntityId, entityCategories, 
                pageSize, addNoteVm, null, null);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                vm.ErrorMessage = errorMessage;
                return View(vm);
            }

            var notes = notesSearchResult.Notes;
            var notesCount = notesSearchResult.NotesCount;


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
            
            vm.Notes = new PaginatedList<NoteViewModel>(
                    notes.Select(x => ConvertNoteDtoToNoteViewModel(x, userId)).ToList(),
                    notesCount, pageIndex, pageSize);
            vm.PreviousPageLink = previousPageLink;
            vm.NextPageLink = nextPageLink;

            return View(vm);
        }

        private NotesViewModel CreateNotesViewModel(string searchedStringInText, int? categoryId,
            string exactDate, string fromDate, string toDate, 
            string creator, NotesSearchByStatus searchByStatus, string sortOption, bool sortIsAscending, 
            List<UserEntityDTO> entities, int selectedEntityId, List<CategoryViewModel> entityCategories, 
            int pageSize, AddNoteViewModel addNoteVm, string previousPageLink, string nextPageLink)
        {
            return new NotesViewModel()
            {
                UserEntites = _mapper.Map<List<UserEntityDTO>, List<UserEntityViewModel>>(entities),
                SelectedEntityId = selectedEntityId,
                Notes = new PaginatedList<NoteViewModel>(new List<NoteViewModel>(), 0, 1, pageSize),
                AddNote = addNoteVm,
                SearchNotes = new SearchNotesViewModel
                {
                    EntityId = selectedEntityId,
                    EntityCategories = entityCategories,
                    Creator = creator,
                    SearchByStatus = searchByStatus,
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
                Category = x.Category != null ? 
                new CategoryViewModel
                {
                    CategoryId = x.Category.CategoryId,
                    Name = x.Category.Name
                }:
                null,
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

        [HttpPost("AddNote")]       
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> AddNote(AddNoteViewModel model)        
        {            
            if (!this.ModelState.IsValid)
            {
                return View(nameof(AddNote), model);
            }

            try
            {               
                await _notesService.AddAsync(model.Note.EntityId, model.Note.NoteUser.Id, model.Note.Text, model.SelectedCategoryId, model.Note.HasStatus);
                return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });
            }
            catch (ArgumentException ex)
            {                
                model.ErrorMessage = ex.Message;
                return View(nameof(AddNote), model);
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
                SelectedCategoryId = note.Category?.CategoryId
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
               return View(nameof(Edit), model);                
            }

            if (model.SelectedCategoryId == 0)
            {
                model.SelectedCategoryId = null;
            }

            try
            {
                await _notesService.EditAsync(model.Note.Id, model.Note.Text,
                    model.SelectedCategoryId, model.Note.IsCompleted,
                    model.Note.HasStatus);
                return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });
            }
            catch (ApplicationException ex)
            {
                model.ErrorMessage = ex.Message;               
                return View(nameof(Edit), model);                
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
                    searchByStatus = model.SearchByStatus,
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
