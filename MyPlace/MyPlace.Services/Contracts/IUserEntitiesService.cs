namespace MyPlace.Services.Contracts
{
    using MyPlace.Data.Models;
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserEntitiesService
    {
        Task<List<UserEntityDTO>> GetAllUserEntitiesAsync(string userId); 
    }
}
