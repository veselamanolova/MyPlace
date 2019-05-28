using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlace.Data.Models;

namespace MyPlace.Data.Repositories
{
    public interface INotesRepository
    {
        Task<Note> AddAsync(Note newNote);
        Task<List<Note>> GetAllAsync(int entityId);
        Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId);
        Task<List<Note>> SerchByTextAsync(int entityId, string searchedString);
    }
}