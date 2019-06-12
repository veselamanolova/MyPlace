
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


