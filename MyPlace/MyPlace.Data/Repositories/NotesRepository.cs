
namespace MyPlace.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.DataModels;

    public class NotesRepository : INotesRepository
    {
        private readonly ApplicationDbContext _context;

        public NotesRepository(ApplicationDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));
        

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

        public async Task<int> CountAsync(int entityId, string searchedString,
        int? categoryId, DateTime? exactDate,
        DateTime? fromDate, DateTime? toDate, string creator)
        {
            IQueryable<Note> query = GenerateSearchQuery(entityId, searchedString, categoryId, exactDate, fromDate, toDate, creator);
            return await query.CountAsync();
        }


        public async Task<List<Note>> SearchAsync(int entityId, string searchedString,
            int? categoryId, DateTime? exactDate,
            DateTime? fromDate, DateTime? toDate, string creator, 
            string sortOption, bool sortIsAscending, int? skip, int? take)
        {
            IQueryable<Note> query = GenerateSearchQuery(entityId, searchedString,
                categoryId, exactDate, fromDate, toDate, creator);
            query = AddSortingToQuery(sortOption, sortIsAscending, query);
            query = AddPagingToQuery(skip, take, query);
            return await query.ToListAsync();
        }

        private static IQueryable<Note> AddPagingToQuery(int? skip, int? take, IQueryable<Note> query)
        {
            if (skip != null && skip > 0)
                query = query.Skip<Note>((int)skip);

            if (take != null && take > 0)
                query = query.Take<Note>((int)take);
            return query;
        }

        private static IQueryable<Note> AddSortingToQuery(string sortOption, bool sortIsAscending, IQueryable<Note> query)
        {
            if (!string.IsNullOrWhiteSpace(sortOption))
            {
                if (sortOption == "Text")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.Text) :
                        query.OrderByDescending(note => note.Text);
                }
                else if (sortOption == "Creator")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.User.UserName) :
                        query.OrderByDescending(note => note.User.UserName);
                }
                else if (sortOption == "Category")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.Category.Name) :
                        query.OrderByDescending(note => note.Category.Name);
                }
                else if (sortOption == "Category")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.Category.Name) :
                        query.OrderByDescending(note => note.Category.Name);
                }
                else if (sortOption == "Satus")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.HasStatus).ThenByDescending(note => note.IsCompleted) :
                        query.OrderByDescending(note => note.HasStatus).ThenByDescending(note => note.IsCompleted);
                }
                else if (sortOption == "Date")
                {
                    query = sortIsAscending ?
                        query.OrderBy(note => note.Date) :
                        query.OrderByDescending(note => note.Date);
                }
                else
                {
                    query = query.OrderByDescending(note => note.Date);
                }
            }
            return query;
        }      


        private IQueryable<Note> GenerateSearchQuery(int entityId, string searchedString, 
            int? categoryId, DateTime? exactDate, DateTime? fromDate, DateTime? toDate, string creator)
        {
            var query = _context.Notes.Where(note => note.EntityId == entityId)
                .Include(note => note.User)
                .Include(note => note.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchedString))
                query = query.Where(note => note.Text.Contains(searchedString));

            //if category is null - no search by category
            //if category is -1 
            if (categoryId != null && categoryId > 0)
                query = query.Where(note => note.CategoryId == categoryId);
            //search whithout category
            if (categoryId == -1)
                query = query.Where(note => note.CategoryId == null);

            if (!string.IsNullOrWhiteSpace(creator))
                query = query.Where(note => note.User.UserName.Contains(creator));

            query = FilterByDates(exactDate, fromDate, toDate, query);
            return query;
        }


        private static IQueryable<Note> FilterByDates(DateTime? exactDate, DateTime? fromDate, DateTime? toDate, IQueryable<Note> query)
        {
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
                    if (toDate != null && fromDate == null)
                        query = query.
                                Where(note => note.Date <= ((DateTime)toDate).Date.AddMinutes(60 * 24));
                }
            }
            return query;
        }
    }
}

