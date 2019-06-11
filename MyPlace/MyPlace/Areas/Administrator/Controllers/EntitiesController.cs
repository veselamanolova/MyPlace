namespace MyPlace.Areas.Administrator.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;   
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Services.Contracts;
    using MyPlace.Areas.Administrator.Models;
    using System.Collections.Generic;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.DataModels;

    [Area("Administrator")]
    public class EntitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEntityService _entityService;
        private readonly ICategoryService _categoryService;         
        private readonly IUserEntitiesService _userEntityService;

        public EntitiesController(IEntityService entityService, ICategoryService categoryService, IMapper mapper, IUserEntitiesService userEntityService)
        {
            _entityService = entityService;
            _categoryService = categoryService;
            _mapper = mapper;
            _userEntityService = userEntityService;
        }        

        [HttpGet("Entities")]
        public async Task<IActionResult> Entities()
        {
            var entities = await _entityService.GetAllEntitiesAsync();
            List<EntityViewModel> result = _mapper.Map<List<EntityViewModel>>(entities);
            EntitiesViewModel vm = new EntitiesViewModel();
            vm.Entities = result; 

            return View(vm); 
        }

        [HttpGet("AdministerEntity")]
        public async Task<IActionResult> AdministerEntity(int Id)
        {
            var entity = await _entityService.GetEntityByIdAsync(Id);         

            var logbooks = entity.LogBooks.ToList();
            var listLogbooks = _mapper.Map<List<LogBookViewModel>>(logbooks);

            var vm = new AdministerEntityViewModel()
            {
                Entity = _mapper.Map<EntityViewModel>(entity),
                LogBooks = listLogbooks
            }; 

            return View(vm);
        }

        [HttpGet("LogBook")]
        public async Task<IActionResult> LogBook(int id)
        {
            var allCategories = await _categoryService.GetAllCategoriesAsync();
            var categoriesvm = _mapper.Map<List<CategoryViewModel>>(allCategories);

            var logbook = await _entityService.GetLogBookByIdAsync(id); 
            var logbookvm = _mapper.Map<LogBookViewModel>(logbook);

            var logBookUsers = await _userEntityService.GetAllEntityUsersAsync(id);
            var allManagers = (await _userEntityService.GetAllUsersInRole("Manager"))
                .Select(x => new SelectableUserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    isSelected = false
                }).ToList(); 

            AdministerLogBookViewModel vm = new AdministerLogBookViewModel()
            {
                LogBook = logbookvm,
                AllCategories = allCategories,
                AllUsers = allManagers,
                EntityUsers = logBookUsers
            };        

            return View(vm);
        }


        

        [HttpPost("AddManagerToLogBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddManagerToLogBook(AdministerLogBookViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(nameof(LogBook), model);
            }


            else
            {


                return RedirectToAction(nameof(LogBook), new { id = model.LogBook.Id});
            }
            //try
            //{
            //    await _notesService.AddAsync(model.Note.EntityId, model.Note.NoteUser.Id, model.Note.Text, model.SelectedCategoryId);
            //    return RedirectToAction(nameof(Notes), new { entityId = model.Note.EntityId });
            //}
            //catch (ArgumentException ex)
            //{
            //    this.ModelState.AddModelError("Error", ex.Message);
            //    return View(nameof(Notes), model);
            //}
        }
    }
}