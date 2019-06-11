using Microsoft.EntityFrameworkCore;
using MyPlace.Data;
using MyPlace.DataModels;
using MyPlace.Services.Contracts;
using MyPlace.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlace.Services
{
    public class EntityService : IEntityService
    {
        private readonly ApplicationDbContext _context;

        public EntityService(ApplicationDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<Entity>> GetAllEntitiesAsync() =>
            await _context.Entities
            .Where(e => e.EstablishmentId == null)
            .ToListAsync();

        public async Task<EntityDTO> GetEntityByIdAsync(int Id)
        {
            var result = await _context.Entities
                .Where(entity => entity.Id == Id)
                .Include(entity => entity.LogBooks)
                .ThenInclude(logbook => logbook.EntityCategories)
                .ThenInclude(ec => ec.Category)
                .FirstOrDefaultAsync();
            return ConvertToDTO(result);
        }

        private static EntityDTO ConvertToDTO(Entity result)
        {
            return new EntityDTO
            {
                Id = result.Id,
                Title = result.Title,
                Address = result.Address,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                EstablishmentId = result.EstablishmentId,
                LogBooks = result.LogBooks?.Select(ConvertToDTO),
                Categories = result.EntityCategories?.Select(x => new CategoryDTO
                {
                    CategoryId = x.CategoryId,
                    Name = x.Category.Name
                })
            };
        }

        public async Task<EntityDTO> GetLogBookByIdAsync(int Id)
        {
            var result =
            await _context.Entities
            .Where(logbook => logbook.Id == Id)
            .Include(logbook => logbook.EntityCategories)
                .ThenInclude(ec => ec.Category)               
            .FirstOrDefaultAsync();

            return ConvertToDTO(result);
        }

       
    }

    
}
