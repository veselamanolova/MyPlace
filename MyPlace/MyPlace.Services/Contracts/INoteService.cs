﻿
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.Data.Models;
    using MyPlace.Services.DTOs;

    public interface INoteService
    {  
        Task<Note> AddAsync(int entityId,  string userId, string text, int? categoryId);

        Task<List<NoteDTO>> SearchAsync(int entityId, string searchedString, int? categoryId); 
    }
}
