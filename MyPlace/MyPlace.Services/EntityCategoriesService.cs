
namespace MyPlace.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Services.DTOs;
    using MyPlace.Services.Contracts;

    public class EntityCategoriesService: IEntityCategoriesService
    {
        private readonly ApplicationDbContext _context;

        public EntityCategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

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
