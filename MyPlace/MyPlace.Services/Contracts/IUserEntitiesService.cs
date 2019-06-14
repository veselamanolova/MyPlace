
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.Services.DTOs;
    using System.Linq;

    public interface IUserEntitiesService
    {
        Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId);

        Task<List<MinUserDTO>> GetAllUsersAsync();

        Task<List<MinUserDTO>> GetAllUsersInRole(string roleName);

        Task<List<MinUserDTO>> GetAllEntityUsersAsync(int entityId);

        Task<CompositeEntityUsersDTO> GetUsersNeededForUsersToEntityAsignmentAsync(int entityId, string roleName);

        Task AssignUsersToEnityAsync(int entityId, string userId);

        Task AssignCategoryToLogbookAsync(int id, int categoryId);
    }
}
