namespace MyPlace.Services
{
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EntityCategoriesService: IEntityCategoriesService
    {
        private readonly ApplicationDbContext _context;

        public EntityCategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EntityCategoryDTO>> GetAllEntityCategoriesAsync(int entityId)
        {
            return await _context.EntityCategories
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
}
