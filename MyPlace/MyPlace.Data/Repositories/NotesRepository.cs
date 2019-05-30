using Microsoft.EntityFrameworkCore;
using MyPlace.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlace.Data.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly ApplicationDbContext _context;

        public NotesRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Note> AddAsync(Note newNote)
        {
            var result = await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();
            return result.Entity;
        }       

        public async Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId) =>
             await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                .Include(ue => ue.Entity)
                .ToListAsync();


        public async Task<List<Note>> SearchAsync(int entityId, string searchedString, int? categoryId)
        {  
            var query = _context.Notes.Where(note => note.EntityId == entityId)
                .Include(note => note.User)
                .Include(note => note.Category)
                .AsQueryable();            
            if (!string.IsNullOrWhiteSpace(searchedString))
                query = query.Where(note => note.Text.Contains(searchedString));

            if (categoryId != null && categoryId > 0)
                query = query.Where(note => note.CategoryId == categoryId); 

            return await query.ToListAsync();
        }
    }
}

