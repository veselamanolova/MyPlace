namespace MyPlace.Services.Contracts
{
    using MyPlace.Data.Models;    
    using System.Collections.Generic;    
    using System.Threading.Tasks;

    public interface IEntityCategoriesService
    {
       Task<List<EntityCategory>> GetAllEntityCategoriesAsync(int entityId); 
    }
}
