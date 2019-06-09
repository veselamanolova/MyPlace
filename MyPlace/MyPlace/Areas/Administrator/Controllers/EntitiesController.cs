namespace MyPlace.Areas.Administrator.Controllers
{
    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;   
    using Microsoft.AspNetCore.Mvc;
    using MyPlace.Services.Contracts;
    using MyPlace.Areas.Administrator.Models;
    using System.Collections.Generic;
    using MyPlace.DataModels;

    [Area("Administrator")]
    public class EntitiesController : Controller
    {
        private readonly IEntityService _entityService;
        private readonly IMapper _mapper;

        public EntitiesController(IEntityService entityService, IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }

        

        [HttpGet("Entities")]
        public async Task<IActionResult> Entities()
        {
            var entities = await _entityService.GetAllEntitiesAsync();
            List<EntityViewModel> result = _mapper.Map<List<EntityViewModel>>(entities);
            EntitiesViewModel vm = new EntitiesViewModel();
            vm.Entities = result; 
            

           // public async Task<IQueryable<EntityCategoryDTO>> GetAllEntityCategoriesAsync(int id) =>
           //    await Task.Run(() => _mapper.ProjectTo<EntityCategoryDTO>(_context.EntityCategories
           //        .Where(entityCategory => entityCategory.EntityId == id)
           //        .Include(entityCategory => entityCategory.Category)));
            return View(vm); 
        }        
      
    }
}