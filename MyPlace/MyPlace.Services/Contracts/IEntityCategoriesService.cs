namespace MyPlace.Services.Contracts
{
    using MyPlace.Services.DTOs;
    using System.Collections.Generic;    
    using System.Threading.Tasks;

    public interface IEntityCategoriesService
    {
       Task<List<EntityCategoryDTO>> GetAllEntityCategoriesAsync(int entityId); 
    }
}
