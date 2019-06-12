
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Services.DTOs;
    using MyPlace.Services.Contracts;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using MyPlace.DataModels;

    public class UserEntitiesService : IUserEntitiesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserEntitiesService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager; 
        }               

        public async Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId) =>
             await _context.UsersEntities
                .Where(ue => ue.UserId == userId)
                .Include(ue => ue.Entity)
                .Select(ue => new UserEntityDTO
                {
                    EntityId = ue.EntityId,
                    UserId = ue.UserId,
                    Title = ue.Entity.Title
                })
                .ToListAsync();

        public async Task<List<MinUserDTO>> GetAllUsersAsync() =>
           await _context.Users
              .Select(ue => new MinUserDTO
              {                  
                  Id = ue.Id,
                  Name = ue.UserName
              })
              .ToListAsync();

        
        //Manager
        //Administrator
        public async Task<List<MinUserDTO>> GetAllUsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users.Select(u => new MinUserDTO
            {
                Id = u.Id,
                Name = u.UserName
            })
            .ToList();
        }      

        public async Task<List<MinUserDTO>> GetAllEntityUsersAsync(int entityId)
        {
            return await _context.UsersEntities
               .Where(ue => ue.EntityId == entityId)
               .Include(ue => ue.User)
               .Select(ue => new MinUserDTO
               {
                   Id = ue.UserId,
                   Name = ue.User.UserName
               })
              .ToListAsync();
        }

        public async Task<CompositeEntityUsersDTO> GetUsersNeededForUsersToEntityAsignmentAsync(int entityId, string roleName)
        {
            var logBookUsers = await GetAllEntityUsersAsync(entityId);
            var allNotLogBookUsers = (await GetAllUsersInRole(roleName))
                .Where(x => !logBookUsers.Any(y => x.Id == y.Id)).ToList();
           return new CompositeEntityUsersDTO()
            {
                EntityUsers= logBookUsers,
                AllNotEntityUsers = allNotLogBookUsers
            };
        }

        public async Task AssignUsersToEnityAsync(int entityId, string userId)
        {
            await _context.UsersEntities.AddAsync(new UserEntity()
            {
                UserId = userId,
                EntityId= entityId
            });
            await _context.SaveChangesAsync();
        }

        public  async Task AssignCategoryToLogbookAsync(int entityId, int categoryId)
        {
            await _context.EntityCategories.AddAsync(new EntityCategory()
            {
                CategoryId = categoryId,
                EntityId = entityId
            });
            await _context.SaveChangesAsync();
        }
    }
    
}

