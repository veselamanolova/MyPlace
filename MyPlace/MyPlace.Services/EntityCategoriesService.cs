
namespace MyPlace.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Services.DTOs;
    using MyPlace.Services.Contracts;
    using AutoMapper;
    using System.Collections.Generic;

    public class EntityCategoriesService : IEntityCategoriesService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public EntityCategoriesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<IQueryable<EntityCategoryDTO>> GetAllEntityCategoriesAsync(int id) =>
        //    await Task.Run(() => _mapper.ProjectTo<EntityCategoryDTO>(_context.EntityCategories
        //        .Where(entityCategory => entityCategory.EntityId == id)
        //        .Include(entityCategory => entityCategory.Category)));

        public async Task<List<EntityCategoryDTO>> GetAllEntityCategoriesAsync(int entityId) =>
        await _context.EntityCategories
          .Where(ec => ec.EntityId == entityId)
          .Include(ec => ec.Category)
          .Select(ec => new EntityCategoryDTO
          {
              CategoryId = ec.CategoryId,
              EntityId = ec.EntityId,
              Name = ec.Category.Name
          })
          .ToListAsync();
    }
}


