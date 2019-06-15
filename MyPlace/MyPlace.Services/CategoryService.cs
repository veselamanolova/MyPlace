
namespace MyPlace.Services
{
    using AutoMapper;
    using System.Linq;
    using MyPlace.Data;
    using MyPlace.Services.DTOs;
    using System.Threading.Tasks;
    using MyPlace.Services.Contracts;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using MyPlace.DataModels;

    public class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }      

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync() =>
           await _context.Categories               
               .Select(c => new CategoryDTO
               {
                   CategoryId = c.Id,                 
                   Name = c.Name
                })
               .ToListAsync();

        public async Task AddCategoryAsync(string name)
        {
            await _context.AddAsync(new Category() { Name = name });                
            await _context.SaveChangesAsync(); 
        }

        public async Task<CategoryDTO> FindCategoryByIdAsync(int id)
        {
            var result =  await _context.Categories.FindAsync(id);
            return new CategoryDTO()
            {
                CategoryId = result.Id,
                Name = result.Name
            }; 
        }

        public async Task EditCategoryAsync(int id, string name)
        {
            Category category= await _context.Categories.FindAsync(id);
            category.Name = name; 
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _context.EntityCategories.RemoveRange(_context.
                EntityCategories.Where(ec => ec.CategoryId == id));

            _context.Remove(_context.
                 Categories.Find(id));

            var affectedNotes = _context.Notes.Where(n => n.CategoryId == id);
            foreach (var note in affectedNotes)
            {
                note.CategoryId = null;
                _context.Update(note);
            }
            await _context.SaveChangesAsync();
        }



        public async Task<List<CategoryDTO>> GetAllLogBooksCategoriesAsync(int id)=>
         await _context.EntityCategories.Where(ec=>ec.EntityId == id)
            .Include(ec => ec.Category)
             .Select(ec => new CategoryDTO
             {
                 CategoryId = ec.Category.Id,
                 Name = ec.Category.Name
             })
             .ToListAsync();


        public async Task<CompositeEntityCategoriesDTO> GetAllEntityAndNotEntityCategories(int id)
        {
            var logBookCategories = await GetAllLogBooksCategoriesAsync(id);
            var allNotLogBookCategories = (await GetAllCategoriesAsync())
                .Where(x => !logBookCategories.Any(y => x.CategoryId == y.CategoryId)).ToList();
            return new CompositeEntityCategoriesDTO()
            {
                EntityCategories = logBookCategories,
                AllNotEntityCategories = allNotLogBookCategories
            };
        }
    }
}


