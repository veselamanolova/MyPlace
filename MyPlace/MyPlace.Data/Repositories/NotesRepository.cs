using Microsoft.EntityFrameworkCore;
using MyPlace.Data.Models;
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

        public async Task<List<Note>> GetAllAsync(int entityId) =>
            await _context.Notes
                .Where(note => note.EntityId == entityId)
                .Include(note => note.User)
                .Include(note => note.Category)                          
                .ToListAsync();

        public async Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId) =>
             await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                .Include(ue => ue.Entity)                
                .ToListAsync();


        public async Task<List<Note>> SerchByTextAsync(int entityId, string searchedString) =>
           await _context.Notes
               .Where(note => note.EntityId == entityId && note.Text.Contains(searchedString))
               .Include(note => note.Category)              
               .ToListAsync();
    }
}

