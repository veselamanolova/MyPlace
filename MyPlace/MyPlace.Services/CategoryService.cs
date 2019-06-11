
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
    }
}


