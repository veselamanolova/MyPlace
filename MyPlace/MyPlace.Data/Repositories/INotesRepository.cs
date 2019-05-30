using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlace.DataModels;

namespace MyPlace.Data.Repositories
{
    public interface INotesRepository
    {
        Task<Note> AddAsync(Note newNote);
        Task<List<UserEntity>> GetAllUserEntitiesAsync(string userId);
        Task<List<Note>> SearchAsync(int entityId, string searchedString, int? categoryId);
    }
}