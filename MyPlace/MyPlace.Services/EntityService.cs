using Microsoft.EntityFrameworkCore;
using MyPlace.Data;
using MyPlace.DataModels;
using MyPlace.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPlace.Services
{
    public class EntityService: IEntityService
    {
        private readonly ApplicationDbContext _context;

        public EntityService(ApplicationDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

         public async Task<List<Entity>> GetAllEntitiesAsync() =>
             await _context.Entities                
                .ToListAsync();
    }

    
}
