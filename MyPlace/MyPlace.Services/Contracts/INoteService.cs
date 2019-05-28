
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.Data.Models;
    using MyPlace.Services.DTOs;

    public interface INoteService
    {
        Task<List<NoteDTO>> GetAllAsync(int entityId);

        Task<Note> AddAsync(int entityId,  string userId, string text, int? categoryId);        
    }
}
