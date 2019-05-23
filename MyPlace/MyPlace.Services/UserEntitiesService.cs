
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.DTOs;
    using MyPlace.Services.Contracts;
    using AutoMapper;
    using System.Collections;

    public class UserEntitiesService : IUserEntitiesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserEntitiesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
               

        public async Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId)
        {
            return await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                .Include(ue => ue.Entity)
                .Select(ue => new UserEntityDTO
                {
                    EntityId = ue.EntityId,
                    UserId = ue.UserId,
                    Title = ue.Entity.Title
                })
                .ToListAsync();
        }
    }
}

