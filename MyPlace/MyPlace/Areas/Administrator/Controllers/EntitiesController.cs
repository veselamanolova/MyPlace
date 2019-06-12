
namespace MyPlace.Areas.Administrator.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MyPlace.Areas.Administrator.Models;
    using MyPlace.Areas.Notes.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Common;
    using System;

    [Area("Administrator")]
    [Authorize(Roles = GlobalConstants.AdminRole)]
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
            EntitiesViewModel vm = new EntitiesViewModel()
            {
                Entities = result
            };          
            return View(vm); 
        }

        [HttpGet("AdministerEntity")]
        public async Task<IActionResult> AdministerEntity(int id)
        {
            var entity = await _entityService.GetEntityByIdAsync(id);         

            var logbooks = entity.LogBooks.ToList();
            var listLogbooks = _mapper.Map<List<LogBookViewModel>>(logbooks);

            var usersNeeededForUserToEntityAssignment = await _userEntityService
                     .GetUsersNeededForUsersToEntityAsignmentAsync(id, "Moderator");
            var entityModerators = usersNeeededForUserToEntityAssignment.EntityUsers;
            var unassignedModerators = usersNeeededForUserToEntityAssignment
                .AllNotEntityUsers.Select(x => new SelectableUserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    isSelected = false
                }).ToList();


            var vm = new AdministerEntityViewModel()
            {
                Entity = _mapper.Map<EntityViewModel>(entity),
                LogBooks = listLogbooks, 
                EntityModerators = entityModerators,
                UnassignedModerators = unassignedModerators
            }; 

            return View(vm);
        }

        [HttpGet("LogBook")]
        public async Task<IActionResult> LogBook(int id)
        {
            var catalogsNeeededForUserToEntityAssignment = await _categoryService
                .GetAllEntityAndNotEntityCategories(id);

            var allNotAssignedCategories = catalogsNeeededForUserToEntityAssignment
                .AllNotEntityCategories.Select(x => new SelectableCategoryViewModel()
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    isSelected = false
                }).ToList(); 

            var logBookCategories = catalogsNeeededForUserToEntityAssignment
                .EntityCategories; 

            var logbook = await _entityService.GetLogBookByIdAsync(id); 
            var logbookvm = _mapper.Map<LogBookViewModel>(logbook);

            var usersNeeededForUserToEntityAssignment = await _userEntityService
                .GetUsersNeededForUsersToEntityAsignmentAsync(id, "Manager");               
            var logBookUsers = usersNeeededForUserToEntityAssignment.EntityUsers;
            var allNotLogBookUsers = usersNeeededForUserToEntityAssignment
                .AllNotEntityUsers.Select(x => new SelectableUserViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                isSelected = false
            }).ToList();

            AdministerLogBookViewModel vm = new AdministerLogBookViewModel()
            {
                LogBook = logbookvm,
                AllUnassignedCategories = allNotAssignedCategories,
                LogBookCategories = logBookCategories,
                AllUnassignedManagers = allNotLogBookUsers,
                LogBookManagers = logBookUsers
            };         

            return View(vm);
        }

        [HttpPost("AddManagerToLogBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddManagerToLogBook(AdministerLogBookViewModel model)
        {            
            try
            {
                foreach (var user in model.AllUnassignedManagers)
                {
                    if (user.isSelected)
                    {
                        await _userEntityService.AssignUsersToEnityAsync(model.LogBook.Id, user.Id);
                    }
                }
               
                return RedirectToAction(nameof(LogBook), new { id = model.LogBook.Id });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(LogBook), model);
            }            
        }

        [HttpPost("AddModeratorToEntity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddModeratorToEntity(AdministerEntityViewModel model)
        {
            try
            {
                foreach (var user in model.UnassignedModerators)
                {
                    if (user.isSelected)
                    {
                        await _userEntityService.AssignUsersToEnityAsync(model.Entity.Id, user.Id);
                    }
                }

                return RedirectToAction(nameof(AdministerEntity), new { id = model.Entity.Id });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(AdministerEntity), model);
            }
        }


        [HttpPost("AddCategoryToLogBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategoryToLogBook(AdministerLogBookViewModel model)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return View(nameof(LogBook), model);
            //}
            try
            {
                foreach (var category in model.AllUnassignedCategories)
                {
                    if (category.isSelected)
                    {
                        await _userEntityService.AssignCategoryToLogbookAsync(model.LogBook.Id, category.CategoryId);
                    }
                }

                return RedirectToAction(nameof(LogBook), new { id = model.LogBook.Id });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(LogBook), model);
            }
        }

    }
}

