
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using MyPlace.Data.Repositories;

    public class NoteService : INoteService
    {
        private readonly INotesRepository _repository;

        public NoteService(INotesRepository repository)
        {
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Note> AddAsync(int entityId, string userId, string text, int? categoryId)
        {
            var newNote = new Note()
            {
                EntityId = entityId,
                UserId = userId,
                Text = text,
                CategoryId = categoryId,
                Date = DateTime.Now,
                IsCompleted = false
            };
           
            return await _repository.AddAsync(newNote);
        }

        public async Task<List<NoteDTO>> GetAllAsync(int entityId) =>
            (await _repository.GetAllAsync(entityId))
                .OrderByDescending(note => note.Date)
                .Select(note => new NoteDTO
                {
                    Id = note.Id,
                    EntityId = note.EntityId,
                    Text = note.Text,
                    Date = note.Date,
                    IsCompleted = note.IsCompleted,
                    User = new NoteUserDTO
                    {
                        Id = note.User.Id,
                        Name = note.User.UserName
                    },
                    Category = new CategoryDTO
                    {
                        Id = note.Category.Id,
                        Name = note.Category.Name
                    }
                })
                .ToList();

        public async Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId) =>
            ( await _repository.GetAllUserEntitiesAsync(userId))                
                .Select(ue => new UserEntityDTO
                {
                    EntityId = ue.EntityId,
                    UserId = ue.UserId,
                    Title = ue.Entity.Title
                })
                .ToList();
       

        public async Task<List<Note>> SerchByTextAsync(int entityId, string searchedString) =>
          ( await _repository.SerchByTextAsync( entityId, searchedString))              
               .OrderByDescending(note => note.Date)
               .ToList();
    }
}
