namespace MyPlace.Services
{
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using MyPlace.Services.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserEntitiesService : IUserEntitiesService
    {
        private readonly ApplicationDbContext _context;

        public UserEntitiesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEntitiesDTO>> GetAllUserEntitiesAsync(string userId)
        {
            List<UserEntity> userEntities = await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                            .Include(ue => ue.Entity)
                            .ToListAsync();

            List<UserEntitiesDTO> userEntitiesDTOsList = new List<UserEntitiesDTO>();

            foreach (var userEntity in userEntities)
            {
                userEntitiesDTOsList.Add(new UserEntitiesDTO()
                {
                    UserId = userEntity.UserId,
                    EntityId = userEntity.EntityId,
                    Title = userEntity.Entity.Title

                });
            }

            return userEntitiesDTOsList;
        }

    }
}
