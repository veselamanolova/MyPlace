namespace MyPlace.Services.Contracts
{
    using MyPlace.DataModels;
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntityService
    {
        Task<List<Entity>> GetAllEntitiesAsync();       

        Task<EntityDTO> GetEntityByIdAsync(int Id);

        Task<EntityDTO> GetLogBookByIdAsync(int Id);
    }
}
