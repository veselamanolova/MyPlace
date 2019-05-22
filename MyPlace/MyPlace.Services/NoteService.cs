
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;

    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        public NoteService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Note> AddAsync(int entityId, string text, int? categoryId)
        {
            var newNote = new Note()
            {
                EntityId = entityId,
                Text = text,
                CategoryId = categoryId,
                Date = DateTime.Now,
                IsCompleted = false
            }; 

            var result = await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();            
            return result.Entity; 
        }

        public async Task<List<Note>> GetAllAsync(int entityId) =>
            await _context.Notes
                .Where(note => note.EntityId == entityId)
                .Include(note => note.Category)
                .ToListAsync();
    }
}
