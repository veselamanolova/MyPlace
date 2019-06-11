
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
    }
}
