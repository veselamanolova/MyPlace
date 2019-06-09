namespace MyPlace.Services.Contracts
{
    using MyPlace.DataModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntityService
    {
        Task<List<Entity>> GetAllEntitiesAsync(); 
    }
}
