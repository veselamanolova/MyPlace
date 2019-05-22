namespace MyPlace.Services
{
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
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

            var result =  await _context.Notes.AddAsync(newNote);

            await _context.SaveChangesAsync();            

            return result.Entity; 
        }

        public async Task<List<Note>> GetAllAsync(int entityId)
        {
            var notesList = await _context.Notes
                            .Where(n => n.EntityId == entityId)
                            .Include(n => n.Category)
                            .ToListAsync();  

            return notesList;
        }

      
    }
}
