
namespace MyPlace.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyPlace.Services.DTOs;

    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
    }
}


