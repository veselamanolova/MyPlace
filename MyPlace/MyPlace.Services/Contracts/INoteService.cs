
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.DataModels;

    public interface INoteService
    {
        Task<List<Note>> GetAllAsync(int entityId);

        Task<Note> AddAsync(int entityId, string text, int? categoryId); 
    }
}
