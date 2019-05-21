

namespace MyPlace.Services.Contracts
{
    using MyPlace.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INoteService
    {
        Task<List<Note>> GetAllAsync(int entityId);

        Task<Note> AddAsync(int entityId, string text, int? categoryId); 
    }
}
