
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
    public class TagsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEntityService _entityService;
        private readonly ICategoryService _categoryService;         


        public TagsController(IEntityService entityService, ICategoryService categoryService, IMapper mapper)
        {
            _entityService = entityService;
            _categoryService = categoryService;
            _mapper = mapper;
        }        

        [HttpGet("Tags")]
        public async Task<IActionResult> Tags()
        {
            var tags = await _categoryService.GetAllCategoriesAsync();
            List<TagViewModel> result = _mapper.Map<List<TagViewModel>>(tags);
            TagsViewModel vm = new TagsViewModel()
            {
                Tags = result,
                AddTag = new TagViewModel() 
            };          
            return View(vm); 
        }

        [HttpPost("AddTag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTag(TagViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _categoryService.AddCategoryAsync(model.Name);
                return RedirectToAction(nameof(Tags));//, new { id = model.LogBook.Id });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Tags), model);
            }
        }

        [HttpGet("EditTag")]
        public async Task<IActionResult> EditTag(int id)
        {

            var result = await _categoryService.FindCategoryByIdAsync(id); 

            TagViewModel model = new TagViewModel
            {
                CategoryId = result.CategoryId,
                Name = result.Name
            };

            return View(nameof(EditTag), model);
        }

        [HttpPost("EditTag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTag(TagViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _categoryService.EditCategoryAsync(model.CategoryId, model.Name);
                return RedirectToAction(nameof(Tags));//, new { id = model.LogBook.Id });
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Tags), model);
            }
        }

        [HttpPost("DeleteTag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTag(TagViewModel model)
        {            
            try
            {
                await _categoryService.DeleteCategoryAsync(model.CategoryId);
                return RedirectToAction(nameof(Tags));
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(nameof(Tags));
            }
        }
    }
}

