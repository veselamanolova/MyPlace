
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.DataModels;
    using MyPlace.Services.DTOs;
    using System;

    public interface INoteService
    {  
        Task<Note> AddAsync(int entityId,  string userId, string text, int? categoryId);

        Task EditAsync(int noteId, string text, int categoryId, bool isCompleted, bool hasStatus);

        Task DeleteAsync(int noteId);

        Task<NoteDTO> GetByIdAsync(int noteId); 

        Task<List<NoteDTO>> SearchAsync(int entityId, string searchedString,
            int? categoryId, DateTime? exactDate, 
            DateTime? startDate, DateTime? endDate, 
            string creator);       
    }
}
