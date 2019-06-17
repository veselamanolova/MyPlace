namespace MyPlace.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyPlace.Common;
    using MyPlace.DataModels;

    public interface INotesRepository
    {
        //Task<Note> AddAsync(Note newNote);

        Task EditAsync(Note note);

        Task DeleteAsync(int noteId);

        Task<Note> GetByIdAsync(int noteId);

        Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId);

        Task<List<Note>> SearchAsync(int entityId, string searchedString, 
            int? categoryId, DateTime? exactDate,
            DateTime? startDate, DateTime? endDate,
            string creator, NotesSearchByStatus searchByStatus, string sortOption, bool sortIsAscending, 
            int? skip, int? take);

        Task<int> CountAsync(int entityId, string searchedString,
          int? categoryId, DateTime? exactDate,
          DateTime? fromDate, DateTime? toDate, string creator, NotesSearchByStatus searchByStatus); 
    }
}