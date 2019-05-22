
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyPlace.Services.DTOs;

    public interface IUserEntitiesService
    {
        Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId); 
    }
}
