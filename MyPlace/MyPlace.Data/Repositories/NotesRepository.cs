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

        public async Task EditAsync(Note note)
        {
            var result =  _context.Update(note);
            await _context.SaveChangesAsync();
            //return result.Entity; 
        }

        public async Task DeleteAsync(int noteId)
        {
            var result =  _context.Notes.Find(noteId);
            _context.Notes.Remove(result);
            await _context.SaveChangesAsync(); 
        }

        public async Task<Note> GetByIdAsync(int noteId)
        {
            var result=  await _context.Notes.
                Include(note=>note.Category).
                Include(note=>note.User).
                Where(note => note.Id == noteId).
                FirstOrDefaultAsync();
            return result; 
        }

        public async Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId) =>
             await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                .Include(ue => ue.Entity)
                .ToListAsync();


        public async Task<List<Note>> SearchAsync(int entityId, string searchedString,
            int? categoryId, DateTime? exactDate,
            DateTime? fromDate, DateTime? toDate, string creator)
        {
            var query = _context.Notes.Where(note => note.EntityId == entityId)
                .Include(note => note.User)
                .Include(note => note.Category)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchedString))
                query = query.Where(note => note.Text.Contains(searchedString));

            if (categoryId != null && categoryId > 0)
                query = query.Where(note => note.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(creator))
            {
                query = query.Where(note => note.User.UserName.Contains(creator));
            }

            if (exactDate != null)
            {
                query = query.Where(note => note.Date >=
                ((DateTime)exactDate).Date &&
                    note.Date <= ((DateTime)exactDate).Date.AddMinutes(60 * 24));
            }

            else
            {
                if (fromDate != null)
                {
                    if (toDate != null)
                    {
                        query = query.
                            Where(note => note.Date >= ((DateTime)fromDate).Date
                            && note.Date <= ((DateTime)toDate).Date.AddMinutes(60 * 24));
                    }
                    else
                    {
                        query = query.
                            Where(note => note.Date >= ((DateTime)fromDate).Date);
                    }
                }
                else 
                {
                    if(toDate != null && fromDate == null)
                    query = query.
                            Where(note => note.Date <= ((DateTime)toDate).Date.AddMinutes(60 * 24));
                }
            }            

            return await query.ToListAsync();
        }        
    }
}

