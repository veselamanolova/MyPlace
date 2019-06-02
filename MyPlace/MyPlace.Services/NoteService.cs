﻿
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using MyPlace.Data.Repositories;

    public class NoteService : INoteService
    {
        private readonly INotesRepository _repository;

        public NoteService(INotesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

        public async Task EditAsync(int noteId, string text, int categoryId, bool isCompleted, bool hasStatus)
        {
            var editableNote = await _repository.GetByIdAsync(noteId);
            editableNote.Text = text;
            editableNote.CategoryId = categoryId;
            editableNote.IsCompleted = isCompleted;
            editableNote.HasStatus = hasStatus;
            await _repository.EditAsync(editableNote); 
        }

        public async Task<NoteDTO> GetByIdAsync(int noteId)
        {
            var note = await _repository.GetByIdAsync(noteId); 
            return ConvertToNoteDTO(note); 
        }


        public async Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId) =>
            (await _repository.GetAllUserEntitiesAsync(userId))
                .Select(ue => new UserEntityDTO
                {
                    EntityId = ue.EntityId,
                    UserId = ue.UserId,
                    Title = ue.Entity.Title
                })
                .ToList();


        public async Task<List<NoteDTO>> SearchAsync(int entityId, string searchedString, int? categoryId,
            DateTime? exactDate, DateTime? fromDate, DateTime? toDate, string creator)
        {
            
            if (fromDate != null && toDate != null)
            {
                if (fromDate > toDate)
                {
                    throw new ArgumentException("From date should be greater than to date."); 
                }
            }

            var result = (await _repository.SearchAsync(entityId, searchedString, categoryId, exactDate, fromDate, toDate, creator))
               .OrderByDescending(note => note.Date)
               .Select(note => ConvertToNoteDTO(note))
               .ToList();
            return result;
        }       

        private static NoteDTO ConvertToNoteDTO(Note note)
        {            
            return new NoteDTO
            {
                Id = note.Id,
                EntityId = note.EntityId,
                Text = note.Text,
                Date = note.Date,
                IsCompleted = note.IsCompleted,
                HasStatus = note.HasStatus,
                NoteUser = new NoteUserDTO
                {
                    Id = note.User.Id,
                    Name = note.User.UserName
                },
                Category = new CategoryDTO
                {
                    CategoryId = note.Category.Id,
                    Name = note.Category.Name
                }
            };
        }        
    }
}
