namespace MyPlace.Services
{
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
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

        public async Task<List<EntityCategory>> GetAllEntityCategoriesAsync(int entityId)
        {
            List<EntityCategory> entityCategories = await _context.EntityCategories
                .Where(ec => ec.EntityId == entityId)
                            .Include(ec => ec.Category)
                            .ToListAsync();         

            return entityCategories;
        }
    }
}
